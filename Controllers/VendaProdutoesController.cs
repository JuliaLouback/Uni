using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Uni.Data;
using Uni.Models;

namespace Uni.Controllers
{
    [Authorize(Roles = "Admin, Gerente, Vendedor")]
    public class VendaProdutoesController : Controller
    {
        private readonly UniContext _context;

        private static List<VendaProduto> listaProduto = new List<VendaProduto>();

        private static int idEdit;

        private static List<VendaProduto> listaExcluido = new List<VendaProduto>();

        // ADD PRODUTO
        public ActionResult AddProduto()
        {
            var values = listaProduto.Select(x => x.Produto.Nome).ToList();
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0).Where(p => !values.Contains(p.Nome)), "Id_produto", "Nome");
            return View();
        }

        public async Task<IActionResult> GerarNota(int id)
        {
            var venda_Produto = await _context.Venda
               .Include(v => v.Cliente).Include(v => v.Funcionario).Include(v => v.Cliente.Endereco).Include(v => v.Cliente.Telefone)
               .FirstOrDefaultAsync(m => m.Id_venda == id);

            listaProduto = _context.VendaProduto.Where(x => x.Venda_Id_venda == id).Include(a => a.Produto).ToList();
            ViewBag.Lista = listaProduto;


            DateTime localDate = DateTime.Now;

            string cultureName = "pt-BR";
            
            var culture = new CultureInfo(cultureName);

            string local = localDate.ToString(culture);
           
            ViewBag.Data = localDate.ToShortDateString();
            ViewBag.Hora = DateTime.Now.ToString("HH:mm");

            Random r = new Random();
            var x = r.Next(0, 999999);
         
            ViewBag.NumeroNota = x.ToString("000000");

            Random protocolo = new Random();
            var protocolos = protocolo.Next(0, 999999999);

            ViewBag.Protocolo = protocolos.ToString("000000000") + x.ToString("000000") + "  " + local;

            decimal valorTotal = 0;
            decimal peso_bruto = 0;
            decimal peso_liquido = 0;

            foreach (VendaProduto produto in ViewBag.Lista)
            {
                decimal valorIcms = 0;
                valorIcms = produto.Valor / Convert.ToDecimal(0.82);
                decimal valorFinal = Math.Round(valorIcms * Convert.ToDecimal(0.18), 2);
                valorTotal = valorTotal + valorFinal;
                peso_bruto = peso_bruto + (Convert.ToDecimal(produto.Produto.Peso_bruto) * produto.Quantidade) ;
                peso_liquido = peso_liquido + (Convert.ToDecimal(produto.Produto.Peso_liquido) * produto.Quantidade);
            }

            ViewBag.ValorIcms = valorTotal;
            ViewBag.PesoB = peso_bruto;
            ViewBag.PesoL = peso_liquido;
           

            string chave = "";

            for(int i = 0; i < 11; i++)
            {
                Random c = new Random();
                var d = r.Next(0, 9999);

                chave = chave + " " +d;
            }

            ViewBag.Chave = chave;

            return View(venda_Produto); 
        }

        //ADD PRODUTO 
        [HttpPost]
        public ActionResult AddProduto(AddProdutoView view)
        {
            if (ModelState.IsValid)
            {
                var produto = _context.Produto.First(a => a.Id_produto == view.ProductId);

                if (view.Quantity > produto.Estoque_atual)
                {
                    return RedirectToAction("ErroProduto", new { produto = produto.Nome, quantidade = produto.Estoque_atual });
                }
                else
                {
                    var pesquisa = listaProduto.Exists(x => x.Produto.Id_produto == view.ProductId);

                    if (pesquisa)
                    {
                        listaProduto.RemoveAll(x => x.Produto_Id_produto == view.ProductId);
                    }
                    VendaProduto vendaProduto = new VendaProduto();
                    vendaProduto.Produto = produto;
                    vendaProduto.Produto_Id_produto = produto.Id_produto;
                    vendaProduto.Quantidade = Convert.ToInt32(view.Quantity);
                    vendaProduto.Valor = decimal.Multiply(Convert.ToDecimal(produto.Valor_unitario), Convert.ToDecimal(view.Quantity));
                    listaProduto.Add(vendaProduto);

                    return RedirectToAction("Create");
                }


            }
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0), "Id_produto", "Nome");
            return View();
        }

        // EDIT PRODUTO
        public ActionResult EditProduto()
        {
            var values = listaProduto.Select(x => x.Produto.Nome).ToList();
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0).Where(p => !values.Contains(p.Nome)), "Id_produto", "Nome");
            ViewBag.Id = idEdit;
            return View();
        }

        //EDIT PRODUTO 
        [HttpPost]
        public ActionResult EditProduto(AddProdutoView view)
        {
            if (ModelState.IsValid)
            {

                var produto = _context.Produto.First(a => a.Id_produto == view.ProductId);

                var pesquisa = listaProduto.Exists(x => x.Produto.Id_produto == view.ProductId);

                if (pesquisa)
                {
                    listaProduto.RemoveAll(x => x.Produto_Id_produto == view.ProductId);
                }

                if (view.Quantity > produto.Estoque_atual)
                {
                    return RedirectToAction("ErroEditProduto", new { produto = produto.Nome, quantidade = produto.Estoque_atual });
                }

                VendaProduto vendaProduto = new VendaProduto();
                vendaProduto.Produto = produto;
                vendaProduto.Produto_Id_produto = produto.Id_produto;
                vendaProduto.Quantidade = Convert.ToInt32(view.Quantity);
                vendaProduto.Valor = decimal.Multiply(Convert.ToDecimal(produto.Valor_unitario), Convert.ToDecimal(view.Quantity));

                produto.Estoque_atual = produto.Estoque_atual - Convert.ToInt32(view.Quantity);
                _context.Produto.Update(produto);
                _context.SaveChanges();

                listaProduto.Add(vendaProduto);

                return RedirectToAction("Edit", new { id = idEdit });
            
        }
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0), "Id_produto", "Nome");
            return View();
        }

        // DELETE PRODUTO
        public ActionResult DeleteProduto(VendaProduto produtos)
        {
            listaProduto.RemoveAll(x => x.Produto_Id_produto == produtos.Produto_Id_produto);
            return RedirectToAction("Create");
        }

        // DELETE EDIT PRODUTO
        public ActionResult DeleteEditProduto(VendaProduto produtos)
        {

            var pesquisa = listaProduto.Find(x => x.Produto_Id_produto == produtos.Produto_Id_produto);
            var produto = pesquisa.Produto;
            produto.Estoque_atual = produto.Estoque_atual + pesquisa.Quantidade;

            _context.Produto.Update(produto);
            _context.SaveChanges();

            listaProduto.RemoveAll(x => x.Produto_Id_produto == produtos.Produto_Id_produto);
            return RedirectToAction("Edit", new { id = idEdit });
        }

        // ERRO PRODUTO - QUANTIDADE MÁXIMA
        public ActionResult ErroProduto(string produto, int quantidade)
        {
            ViewBag.Produto = produto;
            ViewBag.Erro = quantidade;
            return View();
        }

        // ERRO PRODUTO - QUANTIDADE MÁXIMA
        public ActionResult ErroEditProduto(string produto, int quantidade)
        {
            ViewBag.Produto = produto;
            ViewBag.Erro = quantidade;
            return View();
        }

        public VendaProdutoesController(UniContext context)
        {
            _context = context;
        }


        // GET: Venda_Produto
        public async Task<IActionResult> Index()
        {
            listaProduto.Clear();
            var uniContext = _context.Venda.Include(v => v.Cliente).Include(v => v.Funcionario) ;
            return View(await uniContext.ToListAsync());
        }

        // GET: Venda_Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda_Produto = await _context.Venda
                .Include(v => v.Cliente).Include(v => v.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_venda == id);

            listaProduto = _context.VendaProduto.Where(x => x.Venda_Id_venda == id).Include(a => a.Produto).ToList();
            ViewBag.Lista = listaProduto;

            if (venda_Produto == null)
            {
                return NotFound();
            }

            return View(venda_Produto);
        }

        // GET: Venda_Produto/Create
        public IActionResult Create()
        {
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario.Where(x => x.Status == "Ativo"), "Cpf", "Nome");
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Nome");
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda");
            ViewBag.Lista = listaProduto;
            return View();
        }

        // POST: Venda_Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_vendaProduto, Venda")] VendaProduto venda_Produto)
        {

            if (ModelState.IsValid)
            {
                DateTime localDate = DateTime.Now;

                string cultureName = "pt-BR";

                var culture = new CultureInfo(cultureName);

                string local = localDate.ToString(culture);

                Venda venda = new Venda();

                venda.Cliente_Cpf = venda_Produto.Venda.Cliente_Cpf;
                venda.Data_venda = Convert.ToDateTime(local);
                venda.Funcionario_Cpf = venda_Produto.Venda.Funcionario_Cpf;

                decimal total = 0;
                foreach (VendaProduto vendaProduto1 in listaProduto)
                {
                    total = decimal.Add(total, vendaProduto1.Valor);
                }
                venda.Valor_total = total;

                List<VendaProduto> lista = new List<VendaProduto>();
                List<Produto> listaProd = new List<Produto>();

                foreach (VendaProduto vendaProduto in listaProduto)
                {
                    lista.Add(new VendaProduto {
                        Produto_Id_produto = vendaProduto.Produto_Id_produto,
                        Quantidade = vendaProduto.Quantidade,
                        Valor = vendaProduto.Valor,
                        Venda = venda
                    });

                    var produto = _context.Produto.First(a => a.Id_produto == vendaProduto.Produto_Id_produto);
                    produto.Estoque_atual = produto.Estoque_atual - vendaProduto.Quantidade;
                    listaProd.Add(produto);
                }

                _context.AddRange(lista);
                _context.Produto.UpdateRange(listaProd);

                await _context.SaveChangesAsync();
                listaProduto.Clear();
                return RedirectToAction("GerarNota", new { id = venda.Id_venda });
            }
            
            return View(venda_Produto);
        }

        // GET: Venda_Produto/Edit/5
        public async Task<IActionResult> Edit(int? id, int? novo)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            if (novo != null)
            {
                listaProduto = _context.VendaProduto.Where(x => x.Venda_Id_venda == id).Include(a => a.Produto).ToList();
                ViewBag.Lista = listaProduto;
            } else
            {
                ViewBag.Lista = listaProduto;
            }
            

            var venda_Produto = await _context.Venda.FirstOrDefaultAsync(x => x.Id_venda == id);

            if (venda_Produto == null)
            {
                return NotFound();
            }

            idEdit = Convert.ToInt32(id);

            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario.Where(x => x.Status == "Ativo" || x.Cpf == venda_Produto.Funcionario_Cpf), "Cpf", "Nome");
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Nome");
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda");

            return View(venda_Produto);
        }

        // POST: Venda_Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cliente_Cpf,Funcionario_Cpf")] Venda venda_Produto)
        {
            if (id != idEdit)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                DateTime localDate = DateTime.Now;

                string cultureName = "pt-BR";

                var culture = new CultureInfo(cultureName);

                string local = localDate.ToString(culture);

                var vendas = _context.Venda.First(a => a.Id_venda == idEdit);
                vendas.Cliente_Cpf = venda_Produto.Cliente_Cpf;
                vendas.Data_venda = Convert.ToDateTime(local);
                vendas.Funcionario_Cpf = venda_Produto.Funcionario_Cpf;

                decimal total = 0;
                foreach (VendaProduto vendaProduto1 in listaProduto)
                {
                    total = decimal.Add(total, vendaProduto1.Valor);
                }
                vendas.Valor_total = total;

                List<VendaProduto> lista = new List<VendaProduto>();

                foreach (VendaProduto vendaProduto in listaProduto){
                    lista.Add(new VendaProduto {
                        Id_vendaProduto = vendaProduto.Id_vendaProduto,
                        Produto_Id_produto = vendaProduto.Produto_Id_produto,
                        Quantidade = vendaProduto.Quantidade,
                        Valor = vendaProduto.Valor,
                        Venda = vendas
                    });
                }

                _context.Update(vendas);
                _context.RemoveRange(_context.VendaProduto.Where(x => x.Venda_Id_venda == id));
                _context.UpdateRange(lista);
                
                await _context.SaveChangesAsync();
                listaExcluido.Clear();
                return RedirectToAction("GerarNota", new { id = vendas.Id_venda });

            }
           
            return View(venda_Produto);
        }

       
        // GET: Venda_Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda_Produto = await _context.Venda
                .Include(v => v.Cliente).Include(v => v.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_venda == id);

            listaProduto = _context.VendaProduto.Where(x => x.Venda_Id_venda == id).Include(a => a.Produto).ToList();
            ViewBag.Lista = listaProduto;

            if (venda_Produto == null)
            {
                return NotFound();
            }

            return View(venda_Produto);
        }

        // POST: Venda_Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda_Produto = await _context.Venda.FindAsync(id);
            _context.Venda.Remove(venda_Produto);
            _context.VendaProduto.RemoveRange(_context.VendaProduto.Where(x => x.Venda_Id_venda == id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Venda_ProdutoExists(int id)
        {
            return _context.VendaProduto.Any(e => e.Id_vendaProduto == id);
        }
    }
}
