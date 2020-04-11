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
    public class VendaProdutoesController : Controller
    {
        private readonly UniContext _context;

        private static List<VendaProduto> listaProduto = new List<VendaProduto>();

        private static int idEdit;

        public ActionResult AddProduto()
        {
            ViewData["ProductId"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            return View();
        }

        //ADD PRODUTO 
        [HttpPost]
        public ActionResult AddProduto(AddProdutoView view)
        {
            if (ModelState.IsValid)
            {
                var produto = _context.Produto.First(a => a.Id_produto == view.ProductId);
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
            ViewData["ProductId"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            return View();
        }

        public ActionResult EditProduto()
        {
            ViewData["ProductId"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            return View();
        }

        //ADD PRODUTO 
        [HttpPost]
        public ActionResult EditProduto(AddProdutoView view)
        {
            if (ModelState.IsValid)
            {
               
                var pesquisa = listaProduto.Exists(x => x.Produto.Id_produto == view.ProductId);

                if (pesquisa)
                {
                    listaProduto.RemoveAll(x => x.Produto_Id_produto == view.ProductId);
                }

                var produto = _context.Produto.First(a => a.Id_produto == view.ProductId);

                VendaProduto vendaProduto = new VendaProduto();
                vendaProduto.Produto = produto;
                vendaProduto.Produto_Id_produto = produto.Id_produto;
                vendaProduto.Quantidade = Convert.ToInt32(view.Quantity);
                vendaProduto.Valor = decimal.Multiply(Convert.ToDecimal(produto.Valor_unitario), Convert.ToDecimal(view.Quantity));

                System.Diagnostics.Debug.WriteLine("Julia Louback");
                System.Diagnostics.Debug.WriteLine(vendaProduto.Produto_Id_produto);
                System.Diagnostics.Debug.WriteLine("Julia Ribeiro");
                System.Diagnostics.Debug.WriteLine(vendaProduto.Quantidade);

                listaProduto.Add(vendaProduto);

                return RedirectToAction("Edit", new { id = idEdit });
            }
            ViewData["ProductId"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            return View();
        }

        public ActionResult DeleteProduto(VendaProduto produtos)
        {
            // listaProduto.Remove(new Venda_Produto() { Produto_Id_produto = produto.Id_produto });
            //var pesquisa = listaProduto.Find(x => x.Produto.Id_produto == produto.Produto_Id_produto);

            // return View(pesquisa);

            listaProduto.RemoveAll(x => x.Produto_Id_produto == produtos.Produto_Id_produto);
            return RedirectToAction("Create");
        }

        public ActionResult DeleteEditProduto(VendaProduto produtos)
        {
            // listaProduto.Remove(new Venda_Produto() { Produto_Id_produto = produto.Id_produto });
            //var pesquisa = listaProduto.Find(x => x.Produto.Id_produto == produto.Produto_Id_produto);

            // return View(pesquisa);

            listaProduto.RemoveAll(x => x.Produto_Id_produto == produtos.Produto_Id_produto);
            return RedirectToAction("Edit", new { id = idEdit });
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
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Nome");
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Nome");
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda");
            ViewBag.Lista = listaProduto;
            return View();
        }

        // POST: Venda_Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_vendaProduto, Venda")] VendaProduto venda_Produto)
        {

            if (ModelState.IsValid)
            {
               Venda venda = new Venda();

                venda.Cliente_Cpf = venda_Produto.Venda.Cliente_Cpf;
                venda.Data_venda = venda_Produto.Venda.Data_venda;
                venda.Funcionario_Cpf = venda_Produto.Venda.Funcionario_Cpf;

                decimal total = 0;
                foreach (VendaProduto vendaProduto1 in listaProduto)
                {
                    total = decimal.Add(total, vendaProduto1.Valor);
                }
                venda.Valor_total = total;

                List<VendaProduto> lista = new List<VendaProduto>();

                foreach(VendaProduto vendaProduto in listaProduto)
                {
                    lista.Add(new VendaProduto {
                        Produto_Id_produto = vendaProduto.Produto_Id_produto,
                        Quantidade = vendaProduto.Quantidade,
                        Valor = vendaProduto.Valor,
                        Venda = venda
                    });
                }

                _context.AddRange(lista);

                await _context.SaveChangesAsync();
                listaProduto.Clear();
                return RedirectToAction(nameof(Index));
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

            idEdit = Convert.ToInt32(id);

            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Nome");
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Nome");
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda");

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
            
            return View(venda_Produto);
        }

        // POST: Venda_Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    }


                    _context.Update(vendas);
                    _context.RemoveRange(_context.VendaProduto.Where(x => x.Venda_Id_venda == id));
                    _context.UpdateRange(lista);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                
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
