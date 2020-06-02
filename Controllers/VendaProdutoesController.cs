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

        public static AddProdutoView produtoView = new AddProdutoView();

        private static List<VendaProduto> listaExcluido = new List<VendaProduto>();

        // ADD PRODUTO
        public ActionResult AddProduto()
        {
            var values = listaProduto.Select(x => x.Produto.Nome).ToList();
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0).Where(p => !values.Contains(p.Nome)), "Id_produto", "Nome");
            return View();
        }

        public ActionResult AddHistorico(int id)
        {
            ViewData["Data"] = new SelectList(_context.Historico.Where(x => x.Produto.Id_produto == id).OrderByDescending(x => x.Id_historico), "Id_historico", "FullName");
            return View();
        }

        public ActionResult EditHistorico(int id)
        {
            ViewData["Data"] = new SelectList(_context.Historico.Where(x => x.Produto.Id_produto == id).OrderByDescending(x => x.Id_historico), "Id_historico", "FullName");
            return View();
        }

        public ActionResult ErroLista()
        {
            return View();
        }

        public ActionResult ErroListaEdit()
        {
            ViewBag.Editar = idEdit;
            return View();
        }


        [HttpPost]
        public ActionResult AddHistorico(AddProdutoView view)
        {
            var produto = _context.Produto.First(a => a.Id_produto == produtoView.ProductId);

            var pesquisa = listaProduto.Exists(x => x.Produto.Id_produto == produtoView.ProductId);
            var pesquisaPreco = _context.Historico.Where(x => x.Id_historico == view.Id_historico).FirstOrDefault();

            if (pesquisa)
            {
                listaProduto.RemoveAll(x => x.Produto_Id_produto == view.ProductId);
            }

            VendaProduto vendaProduto = new VendaProduto();
            vendaProduto.Valor_unitario = Convert.ToString(pesquisaPreco.Valor);
            vendaProduto.Produto = produto;
            vendaProduto.Produto_Id_produto = produto.Id_produto;
            vendaProduto.Quantidade = Convert.ToInt32(produtoView.Quantity);
            vendaProduto.Valor = decimal.Multiply(Convert.ToDecimal(pesquisaPreco.Valor), Convert.ToDecimal(produtoView.Quantity));
            listaProduto.Add(vendaProduto);

            return RedirectToAction("Create");
            
        }

        [HttpPost]
        public ActionResult EditHistorico(AddProdutoView view)
        {
            var produto = _context.Produto.First(a => a.Id_produto == produtoView.ProductId);

            var pesquisa = listaProduto.Exists(x => x.Produto.Id_produto == view.ProductId);
            var pesquisaPreco = _context.Historico.Where(x => x.Id_historico == view.Id_historico).FirstOrDefault();

            if (pesquisa)
            {
                listaProduto.RemoveAll(x => x.Produto_Id_produto == view.ProductId);
            }

            if (view.Quantity > produto.Estoque_atual)
            {
                return RedirectToAction("ErroEditProduto", new { produto = produto.Nome, quantidade = produto.Estoque_atual });
            }

            VendaProduto vendaProduto = new VendaProduto();
            vendaProduto.Valor_unitario = Convert.ToString(pesquisaPreco.Valor);
            vendaProduto.Produto = produto;
            vendaProduto.Produto_Id_produto = produto.Id_produto;
            vendaProduto.Quantidade = Convert.ToInt32(produtoView.Quantity);
            vendaProduto.Valor = decimal.Multiply(Convert.ToDecimal(pesquisaPreco.Valor), Convert.ToDecimal(produtoView.Quantity));

            produto.Estoque_atual = produto.Estoque_atual - Convert.ToInt32(view.Quantity);
            _context.Produto.Update(produto);
            _context.SaveChanges();

            listaProduto.Add(vendaProduto);

            return RedirectToAction("Edit", new { id = idEdit });

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
                    produtoView = view;
                    return RedirectToAction("AddHistorico", new { id = view.ProductId });
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

                if (view.Quantity > produto.Estoque_atual)
                {
                    return RedirectToAction("ErroProduto", new { produto = produto.Nome, quantidade = produto.Estoque_atual });
                }
                else
                {
                    produtoView = view;
                    return RedirectToAction("EditHistorico", new { id = view.ProductId });
                }
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

        //Add aqui 
        public async Task<IActionResult> Index(string funcionario, string cliente, string dataIni, string dataFin, int? page)
        {
            listaProduto.Clear();
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Nome");
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Nome");

            System.Diagnostics.Debug.WriteLine(dataIni);

            var vendaProdutos = from m in _context.Venda.Include(v => v.Cliente).Include(v => v.Funcionario).OrderByDescending(x => x.Id_venda)
                               select m;

            if (!string.IsNullOrEmpty(funcionario))
            {
                vendaProdutos = vendaProdutos.Where(s => s.Funcionario_Cpf == funcionario);
            }

            if (!string.IsNullOrEmpty(cliente))
            {
                vendaProdutos = vendaProdutos.Where(s => s.Cliente_Cpf == cliente);
            }

            if (!string.IsNullOrEmpty(dataIni) && !string.IsNullOrEmpty(dataFin))
            {
                var date = Convert.ToDateTime(dataIni).Date;
                var date1 = Convert.ToDateTime(dataFin).Date;
                var nextDay = date1.AddDays(1);

                vendaProdutos = vendaProdutos.Where(s => s.Data_venda >= date && s.Data_venda < nextDay).OrderBy(x => x.Id_venda);
            } else if(!string.IsNullOrEmpty(dataIni)){
                var date = Convert.ToDateTime(dataIni).Date;
                var nextDay = date.AddDays(1);
                vendaProdutos = vendaProdutos.Where(s => s.Data_venda >= date && s.Data_venda < nextDay);
            }

            int PageSize = 4;
            int TotalCount = vendaProdutos.ToList().Count;
            int TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            if (page == null)
            {
                ViewBag.Page = 1;
            }
            else
            {
                ViewBag.Page = page + 1;
            }
            ViewBag.Total = TotalPages;
            ViewBag.Funcionario = funcionario;
            ViewBag.Cliente = cliente;
            ViewBag.Data_venda = dataIni;
            ViewBag.Data_venda = dataFin;

            return View(await vendaProdutos.Skip((page ?? 0) * PageSize).Take(PageSize).ToListAsync());

            /* return View(await vendaProdutos.ToListAsync());*/
        }
        

        
        /*public async Task<IActionResult> Index()
        {
            listaProduto.Clear();
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Nome");
            var uniContext = _context.Venda.Include(v => v.Cliente).Include(v => v.Funcionario) ;
            return View(await uniContext.ToListAsync());
        }*/
        
        

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
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario.Where(x => x.Status == "Ativo" && x.Cargo == "Vendedor"), "Cpf", "Nome");
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
            if(listaProduto.Count == 0)
            {
                return RedirectToAction("ErroLista");
            }

            if (ModelState.IsValid)
            {
                DateTime localDate = DateTime.Now;

                // string cultureName = "pt-BR";

                // var culture = new CultureInfo(cultureName);

                // string local = localDate.ToString(culture);

                Venda venda = new Venda();

                venda.Cliente_Cpf = venda_Produto.Venda.Cliente_Cpf;
                venda.Data_venda = localDate;
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
                        Valor_unitario = vendaProduto.Valor_unitario,
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

            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario.Where(x => x.Status == "Ativo" || x.Cpf == venda_Produto.Funcionario_Cpf && x.Cargo == "Vendedor"), "Cpf", "Nome");
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

            if (listaProduto.Count == 0)
            {
                return RedirectToAction("ErroListaEdit");
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
                        Valor_unitario = vendaProduto.Valor_unitario,
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
                peso_bruto = peso_bruto + (Convert.ToDecimal(produto.Produto.Peso_bruto) * produto.Quantidade);
                peso_liquido = peso_liquido + (Convert.ToDecimal(produto.Produto.Peso_liquido) * produto.Quantidade);
            }

            ViewBag.ValorIcms = valorTotal;
            ViewBag.PesoB = peso_bruto;
            ViewBag.PesoL = peso_liquido;


            string chave = "";

            for (int i = 0; i < 11; i++)
            {
                Random c = new Random();
                var d = r.Next(0, 9999);

                chave = chave + " " + d;
            }

            ViewBag.Chave = chave;

            decimal valorVenda = listaProduto[0].Venda.Valor_total;

            ViewBag.ValorSeguro = Math.Round((valorVenda * Convert.ToDecimal(0.5)) / 100, 2);
            ViewBag.ValorOutros = Math.Round((valorVenda * 1) / 100, 2);
            ViewBag.ValorIPI = Math.Round((valorVenda * Convert.ToDecimal(0.4)) / 100, 2); ;
            ViewBag.ValorAprox = Math.Round((valorVenda * Convert.ToDecimal(0.3)) / 100, 2);
            ViewBag.TotalNota = valorVenda + ViewBag.ValorSeguro + ViewBag.ValorOutros + ViewBag.ValorIPI + ViewBag.ValorAprox;

            return View(venda_Produto);
        }
    }
}
