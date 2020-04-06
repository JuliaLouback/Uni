using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uni.Data;
using Uni.Models;

namespace Uni.Controllers
{
    public class Cliente_VendaController : Controller
    {
        private readonly UniContext _context;

        public Cliente_VendaController(UniContext context)
        {
            _context = context;
        }

        // GET: Cliente_Venda
        public async Task<IActionResult> Index()
        {
            var uniContext = _context.Cliente_Venda.Include(c => c.Cliente).Include(c => c.Venda);
            return View(await uniContext.ToListAsync());
        }

        // GET: Cliente_Venda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente_Venda = await _context.Cliente_Venda
                .Include(c => c.Cliente)
                .Include(c => c.Venda)
                .FirstOrDefaultAsync(m => m.Id_clienteVenda == id);
            if (cliente_Venda == null)
            {
                return NotFound();
            }

            return View(cliente_Venda);
        }

        // GET: Cliente_Venda/Create
        public IActionResult Create()
        {
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Email");
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda");
            return View();
        }

        // POST: Cliente_Venda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_clienteVenda,Cliente_Cpf,Venda_Id_venda")] Cliente_Venda cliente_Venda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente_Venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Email", cliente_Venda.Cliente_Cpf);
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda", cliente_Venda.Venda_Id_venda);
            return View(cliente_Venda);
        }

        // GET: Cliente_Venda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente_Venda = await _context.Cliente_Venda.FindAsync(id);
            if (cliente_Venda == null)
            {
                return NotFound();
            }
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Email", cliente_Venda.Cliente_Cpf);
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda", cliente_Venda.Venda_Id_venda);
            return View(cliente_Venda);
        }

        // POST: Cliente_Venda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_clienteVenda,Cliente_Cpf,Venda_Id_venda")] Cliente_Venda cliente_Venda)
        {
            if (id != cliente_Venda.Id_clienteVenda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente_Venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Cliente_VendaExists(cliente_Venda.Id_clienteVenda))
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
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Email", cliente_Venda.Cliente_Cpf);
            ViewData["Venda_Id_venda"] = new SelectList(_context.Venda, "Id_venda", "Id_venda", cliente_Venda.Venda_Id_venda);
            return View(cliente_Venda);
        }

        // GET: Cliente_Venda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente_Venda = await _context.Cliente_Venda
                .Include(c => c.Cliente)
                .Include(c => c.Venda)
                .FirstOrDefaultAsync(m => m.Id_clienteVenda == id);
            if (cliente_Venda == null)
            {
                return NotFound();
            }

            return View(cliente_Venda);
        }

        // POST: Cliente_Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente_Venda = await _context.Cliente_Venda.FindAsync(id);
            _context.Cliente_Venda.Remove(cliente_Venda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Cliente_VendaExists(int id)
        {
            return _context.Cliente_Venda.Any(e => e.Id_clienteVenda == id);
        }
    }
}
