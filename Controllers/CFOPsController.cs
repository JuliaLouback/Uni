using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uni.Data;
using Uni.Models;

namespace Uni.Controllers
{
    [Authorize(Roles = "Admin, Gerente")]
    public class CFOPsController : Controller
    {
        private readonly UniContext _context;

        public CFOPsController(UniContext context)
        {
            _context = context;
        }

        // GET: CFOPs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CFOP.ToListAsync());
        }

        // GET: CFOPs/Details/5
        public async Task<IActionResult> Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cFOP = await _context.CFOP
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (cFOP == null)
            {
                return NotFound();
            }

            return View(cFOP);
        }

        // GET: CFOPs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CFOPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Descricao")] CFOP cFOPs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cFOPs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cFOPs);
        }

        // GET: CFOPs/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cFOP = await _context.CFOP.FindAsync(id);
            if (cFOP == null)
            {
                return NotFound();
            }
            return View(cFOP);
        }

        // POST: CFOPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Codigo,Descricao")] CFOP cFOPs)
        {
            if (id != cFOPs.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cFOPs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CFOPExists(cFOPs.Codigo))
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
            return View(cFOPs);
        }

        // GET: CFOPs/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cFOP = await _context.CFOP
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (cFOP == null)
            {
                return NotFound();
            }

            return View(cFOP);
        }

        // POST: CFOPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cFOP = await _context.CFOP.FindAsync(id);
            _context.CFOP.Remove(cFOP);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CFOPExists(long id)
        {
            return _context.CFOP.Any(e => e.Codigo == id);
        }
    }
}
