using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uni.Data;
using Uni.Models;

namespace Uni.Controllers
{
    [Authorize(Roles = "Admin, Gerente, Vendedor")]
    public class CotacaoProdutoesController : Controller
    {
        private readonly UniContext _context;

        private static List<VendaProduto> listaProduto = new List<VendaProduto>();

        private static int idEdit;

        private static List<VendaProduto> listaExcluido = new List<VendaProduto>();


        // ADD PRODUTO
        public ActionResult AddProduto()
        {
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0), "Id_produto", "Nome");
            return View();
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
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0), "Id_produto", "Nome");
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
                    var pesquisa1 = listaProduto.Find(x => x.Produto.Id_produto == view.ProductId);

                    produto.Estoque_atual = produto.Estoque_atual + pesquisa1.Quantidade;

                    _context.Produto.Update(produto);
                    _context.SaveChanges();

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
            listaExcluido.Add(pesquisa);

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

        public CotacaoProdutoesController(UniContext context)
        {
            _context = context;
        }


        // GET: Cotação_Produto
        public async Task<IActionResult> Index()
        {
            listaProduto.Clear();
            var uniContext = _context.Venda.Include(v => v.Cliente).Include(v => v.Funcionario);
            return View(await uniContext.ToListAsync());
        }

        // GET: Cotação_Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotacao_Produto = await _context.Cotacao
                .Include(v => v.Cliente).Include(v => v.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_cotacao == id);

            //Aqui muda ou não?
            listaProduto = _context.VendaProduto.Where(x => x.Venda_Id_venda == id).Include(a => a.Produto).ToList();
            ViewBag.Lista = listaProduto;

            if (cotacao_Produto == null)
            {
                return NotFound();
            }

            return View(cotacao_Produto);
        }

        // GET: Cotação_Produto/Create
        public IActionResult Create()
        {
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Nome");
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Nome");
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            ViewData["Cotacao_Id_cotacao"] = new SelectList(_context.Venda, "Id_cotacao", "Id_cotacao");
            ViewBag.Lista = listaProduto;
            return View();
        }

        // POST: Cotação_Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_cotacaoProduto, Venda")] CotacaoProduto cotacao_Produto)
        {

            if (ModelState.IsValid)
            {
                Cotacao cotacao = new Cotacao();

                cotacao.Cliente_Cpf = cotacao_Produto.Cotacao.Cliente_Cpf;
                cotacao.Data_venda = cotacao_Produto.Cotacao.Data_venda;
                cotacao.Funcionario_Cpf = cotacao_Produto.Cotacao.Funcionario_Cpf;

                decimal total = 0;
                foreach (VendaProduto vendaProduto1 in listaProduto)
                {
                    total = decimal.Add(total, vendaProduto1.Valor);
                }
                cotacao.Valor_total = total;

                List<VendaProduto> lista = new List<VendaProduto>();
                List<Produto> listaProd = new List<Produto>();

                foreach (VendaProduto vendaProduto in listaProduto)
                {
                    lista.Add(new VendaProduto
                    {
                        Produto_Id_produto = vendaProduto.Produto_Id_produto,
                        Quantidade = vendaProduto.Quantidade,
                        Valor = vendaProduto.Valor,
                       // Venda = venda
                    });

                    var produto = _context.Produto.First(a => a.Id_produto == vendaProduto.Produto_Id_produto);
                    produto.Estoque_atual = produto.Estoque_atual - vendaProduto.Quantidade;
                    listaProd.Add(produto);
                }

                _context.AddRange(lista);
                _context.Produto.UpdateRange(listaProd);

                await _context.SaveChangesAsync();
                listaProduto.Clear();
                return RedirectToAction(nameof(Index));
            }

            return View(cotacao_Produto);
        }

        // GET: Cotação_Produto/Edit/5
        public async Task<IActionResult> Edit(int? id, int? novo)
        {
            if (id == null)
            {
                return NotFound();
            }

            idEdit = Convert.ToInt32(id);

            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Nome");
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Nome");
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            ViewData["Cotacao_Id_cotacao"] = new SelectList(_context.Venda, "Id_cotacao", "Id_cotacao");

            if (novo != null)
            {
                listaProduto = _context.VendaProduto.Where(x => x.Venda_Id_venda == id).Include(a => a.Produto).ToList();
                ViewBag.Lista = listaProduto;
            }
            else
            {
                ViewBag.Lista = listaProduto;
            }


            var cotacao_Produto = await _context.Cotacao.FirstOrDefaultAsync(x => x.Id_cotacao == id);

            if (cotacao_Produto == null)
            {
                return NotFound();
            }

            return View(cotacao_Produto);
        }

        // POST: Cotação_Produto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cliente_Cpf,Funcionario_Cpf,Data_venda")] Venda venda_Produto)
        {
            if (id != idEdit)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var vendas = _context.Venda.First(a => a.Id_venda == idEdit);
                vendas.Cliente_Cpf = venda_Produto.Cliente_Cpf;
                vendas.Data_venda = venda_Produto.Data_venda;
                vendas.Funcionario_Cpf = venda_Produto.Funcionario_Cpf;

                decimal total = 0;
                foreach (VendaProduto vendaProduto1 in listaProduto)
                {
                    total = decimal.Add(total, vendaProduto1.Valor);
                }
                vendas.Valor_total = total;

                List<VendaProduto> lista = new List<VendaProduto>();
                List<Produto> listaProd = new List<Produto>();


                foreach (VendaProduto i in listaExcluido)
                {
                    var pesquisa = listaProduto.Exists(x => x.Produto.Id_produto == i.Produto_Id_produto);
                    if (!pesquisa)
                    {
                        var produto = _context.Produto.First(a => a.Id_produto == i.Produto_Id_produto);
                        produto.Estoque_atual = produto.Estoque_atual + i.Quantidade;
                        listaProd.Add(produto);
                        System.Diagnostics.Debug.WriteLine(i.Quantidade);
                    }
                }

                foreach (VendaProduto vendaProduto in listaProduto)
                {
                    lista.Add(new VendaProduto
                    {
                        Id_vendaProduto = vendaProduto.Id_vendaProduto,
                        Produto_Id_produto = vendaProduto.Produto_Id_produto,
                        Quantidade = vendaProduto.Quantidade,
                        Valor = vendaProduto.Valor,
                        Venda = vendas
                    });

                    var produto = _context.Produto.First(a => a.Id_produto == vendaProduto.Produto_Id_produto);
                    produto.Estoque_atual = produto.Estoque_atual - vendaProduto.Quantidade;
                    listaProd.Add(produto);
                    System.Diagnostics.Debug.WriteLine("teste2");
                }

                _context.Update(vendas);
                _context.RemoveRange(_context.VendaProduto.Where(x => x.Venda_Id_venda == id));
                _context.UpdateRange(lista);
                _context.Produto.UpdateRange(listaProd);

                await _context.SaveChangesAsync();
                listaExcluido.Clear();
                return RedirectToAction(nameof(Index));

            }

            return View(venda_Produto);
        }


        // GET: Cotação_Produto/Delete/5
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

        // POST: Cotação_Produto/Delete/5
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
