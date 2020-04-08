using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sitecore.FakeDb;
using Uni.Data;
using Uni.Models;

namespace Uni.Controllers
{
    public class Venda_ProdutoController : Controller
    {
        private readonly UniContext _context;
       

        //ADD PRODUTO 
        public ActionResult AddProduto()
        {
          /*  var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.Venda_Id_venda = new SelectList(CombosHelper.GetVendas(user.CompanyId), "IdVenda", "Descricao"); */
            return View();
        }


        public Venda_ProdutoController(UniContext context)
        {
            _context = context;
        }

        // GET: Venda_Produto
        public async Task<IActionResult> Index()
        {
            var uniContext = _context.Venda_Produto.Include(v => v.Venda);
            return View(await uniContext.ToListAsync());
        }

        // GET: Venda_Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda_Produto = await _context.Venda_Produto
                .Include(v => v.Venda)
                .FirstOrDefaultAsync(m => m.Id_vendaProduto == id);
            if (venda_Produto == null)
            {
                return NotFound();
            }

            return View(venda_Produto);
        }

        // GET: Venda_Produto/Create
        public IActionResult Create()
        {
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda");
            return View();
        }

        // POST: Venda_Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_vendaProduto,Quantidade,Valor,Venda_Id_venda,Produto_Id_produto, Produto")] Venda_Produto venda_Produto)
        {
         
            if (ModelState.IsValid)
            {
                _context.Add(venda_Produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda", venda_Produto.Venda_Id_venda);
            return View(venda_Produto);
        }

        // GET: Venda_Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda_Produto = await _context.Venda_Produto.FindAsync(id);
            if (venda_Produto == null)
            {
                return NotFound();
            }
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda", venda_Produto.Venda_Id_venda);
            return View(venda_Produto);
        }

        // POST: Venda_Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_vendaProduto,Quantidade,Valor,Venda_Id_venda,Produto_Id_produto")] Venda_Produto venda_Produto)
        {
            if (id != venda_Produto.Id_vendaProduto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda_Produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Venda_ProdutoExists(venda_Produto.Id_vendaProduto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda", venda_Produto.Venda_Id_venda);
            return View(venda_Produto);
        }

        // GET: Venda_Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda_Produto = await _context.Venda_Produto
                .Include(v => v.Venda)
                .FirstOrDefaultAsync(m => m.Id_vendaProduto == id);
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
            var venda_Produto = await _context.Venda_Produto.FindAsync(id);
            _context.Venda_Produto.Remove(venda_Produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Venda_ProdutoExists(int id)
        {
            return _context.Venda_Produto.Any(e => e.Id_vendaProduto == id);
        }
    }
}
