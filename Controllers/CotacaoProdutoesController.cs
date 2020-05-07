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
    public class CotacaoProdutoesController : Controller
    {
        private readonly UniContext _context;

        private static List<CotacaoProduto> listaProduto = new List<CotacaoProduto>();

        private static int idEdit;

        private static List<CotacaoProduto> listaExcluido = new List<CotacaoProduto>();


        // ADD PRODUTO
        public ActionResult AddProd()
        {
            var values = listaProduto.Select(x => x.Produto.Nome).ToList();
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0).Where(p => !values.Contains(p.Nome)), "Id_produto", "Nome");
            return View();
        }

        //ADD PRODUTO 
        [HttpPost]
        public ActionResult AddProd(AddProdutoView view)
        {
            if (ModelState.IsValid)
            {
                var produto = _context.Produto.First(a => a.Id_produto == view.ProductId);

                if (view.Quantity > produto.Estoque_atual)
                {
                    return RedirectToAction("ErroProd", new { produto = produto.Nome, quantidade = produto.Estoque_atual });
                }
                else
                {
                    var pesquisa = listaProduto.Exists(x => x.Produto.Id_produto == view.ProductId);

                    if (pesquisa)
                    {
                        listaProduto.RemoveAll(x => x.Produto_Id_produto == view.ProductId);
                    }
                    CotacaoProduto vendaProduto = new CotacaoProduto();
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
        public ActionResult EditProd()
        {
            var values = listaProduto.Select(x => x.Produto.Nome).ToList();
            ViewData["ProductId"] = new SelectList(_context.Produto.Where(x => x.Estoque_atual > 0).Where(p => !values.Contains(p.Nome)), "Id_produto", "Nome");
            ViewBag.Id = idEdit;
            return View();
        }

        //EDIT PRODUTO 
        [HttpPost]
        public ActionResult EditProd(AddProdutoView view)
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
                    return RedirectToAction("ErroEditProd", new { produto = produto.Nome, quantidade = produto.Estoque_atual });
                }


                CotacaoProduto vendaProduto = new CotacaoProduto();
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
        public ActionResult DeleteProd(VendaProduto produtos)
        {
            listaProduto.RemoveAll(x => x.Produto_Id_produto == produtos.Produto_Id_produto);
            return RedirectToAction("Create");
        }

        // DELETE EDIT PRODUTO
        public ActionResult DeleteEditProd(VendaProduto produtos)
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
        public ActionResult ErroProd(string produto, int quantidade)
        {
            ViewBag.Produto = produto;
            ViewBag.Erro = quantidade;
            return View();
        }

        // ERRO PRODUTO - QUANTIDADE MÁXIMA
        public ActionResult ErroEditProd(string produto, int quantidade)
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
            var uniContext = _context.Cotacao.Include(v => v.Cliente).Include(v => v.Funcionario);
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
            listaProduto = _context.CotacaoProduto.Where(x => x.Cotacao_Id_cotacao == id).Include(a => a.Produto).ToList();
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
            ViewData["Cotacao_Id_cotacao"] = new SelectList(_context.Cotacao, "Id_cotacao", "Id_cotacao");
            ViewBag.Lista = listaProduto;
            return View();
        }

        // POST: Cotação_Produto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_cotacaoProduto, Cotacao")] CotacaoProduto cotacao_Produto)
        {

            if (ModelState.IsValid)
            {

                DateTime localDate = DateTime.Now;

                string cultureName = "pt-BR";

                var culture = new CultureInfo(cultureName);

                string local = localDate.ToString(culture);

                Cotacao cotacao = new Cotacao();

                cotacao.Cliente_Cpf = cotacao_Produto.Cotacao.Cliente_Cpf;
                cotacao.Data_venda = Convert.ToDateTime(local);
                cotacao.Funcionario_Cpf = cotacao_Produto.Cotacao.Funcionario_Cpf;

                decimal total = 0;
                foreach (CotacaoProduto vendaProduto1 in listaProduto)
                {
                    total = decimal.Add(total, vendaProduto1.Valor);
                }
                cotacao.Valor_total = total;

                List<CotacaoProduto> lista = new List<CotacaoProduto>();
                List<Produto> listaProd = new List<Produto>();

                foreach (CotacaoProduto vendaProduto in listaProduto)
                {
                    lista.Add(new CotacaoProduto
                    {
                        Produto_Id_produto = vendaProduto.Produto_Id_produto,
                        Quantidade = vendaProduto.Quantidade,
                        Valor = vendaProduto.Valor,
                        Cotacao = cotacao 
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
                listaProduto = _context.CotacaoProduto.Where(x => x.Cotacao_Id_cotacao == id).Include(a => a.Produto).ToList();
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
        public async Task<IActionResult> Edit(int id, [Bind("Cliente_Cpf,Funcionario_Cpf")] Cotacao cotacao_Produto)
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

                var cotacao = _context.Cotacao.First(a => a.Id_cotacao == idEdit);
                cotacao.Cliente_Cpf = cotacao_Produto.Cliente_Cpf;
                cotacao.Data_venda = Convert.ToDateTime(local);
                cotacao.Funcionario_Cpf = cotacao_Produto.Funcionario_Cpf;

                decimal total = 0;
                foreach (CotacaoProduto vendaProduto1 in listaProduto)
                {
                    total = decimal.Add(total, vendaProduto1.Valor);
                }
                cotacao.Valor_total = total;

                List<CotacaoProduto> lista = new List<CotacaoProduto>();

                foreach (CotacaoProduto vendaProduto in listaProduto)
                {
                    lista.Add(new CotacaoProduto
                    {
                        Cotacao_Id_cotacao = vendaProduto.Cotacao_Id_cotacao,
                        Produto_Id_produto = vendaProduto.Produto_Id_produto,
                        Quantidade = vendaProduto.Quantidade,
                        Valor = vendaProduto.Valor,
                        Cotacao = cotacao
                    });

                }

                _context.Update(cotacao);
                _context.RemoveRange(_context.CotacaoProduto.Where(x => x.Cotacao_Id_cotacao == id));
                _context.UpdateRange(lista);

                await _context.SaveChangesAsync();
                listaExcluido.Clear();
                return RedirectToAction(nameof(Index));

            }

            return View(cotacao_Produto);
        }


        // GET: Cotação_Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotacao_Produto = await _context.Cotacao
                .Include(v => v.Cliente).Include(v => v.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_cotacao == id);

            listaProduto = _context.CotacaoProduto.Where(x => x.Cotacao_Id_cotacao == id).Include(a => a.Produto).ToList();
            ViewBag.Lista = listaProduto;

            if (cotacao_Produto == null)
            {
                return NotFound();
            }

            return View(cotacao_Produto);
        }

        // POST: Cotação_Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cotacao_Produto = await _context.Cotacao.FindAsync(id);
            _context.Cotacao.Remove(cotacao_Produto);
            _context.CotacaoProduto.RemoveRange(_context.CotacaoProduto.Where(x => x.Cotacao_Id_cotacao == id));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Cotacao_ProdutoExists(int id)
        {
            return _context.CotacaoProduto.Any(e => e.Cotacao_Id_cotacao == id);
        }
    }
}
