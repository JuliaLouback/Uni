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
    public class CSTsController : Controller
    {
        private readonly UniContext _context;

        public CSTsController(UniContext context)
        {
            _context = context;
        }

        // GET: CSTs
        public async Task<IActionResult> Index(int? page)
        {
            var teste = _context.CST;

            int PageSize = 5;
            int TotalCount = teste.ToList().Count;
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

            return View(await teste.Skip((page ?? 0) * PageSize).Take(PageSize).ToListAsync());
        }

        // GET: CSTs/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cST = await _context.CST
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (cST == null)
            {
                return NotFound();
            }

            return View(cST);
        }

        // GET: CSTs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CSTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Descricao")] CST cST)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cST);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cST);
        }

        // GET: CSTs/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cST = await _context.CST.FindAsync(id);
            if (cST == null)
            {
                return NotFound();
            }
            return View(cST);
        }

        // POST: CSTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Descricao")] CST cST)
        {
            if (id != cST.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cST);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CSTExists(cST.Codigo))
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
            return View(cST);
        }

        // GET: CSTs/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cST = await _context.CST
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (cST == null)
            {
                return NotFound();
            }

            return View(cST);
        }

        // POST: CSTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cST = await _context.CST.FindAsync(id);
            _context.CST.Remove(cST);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CSTExists(string id)
        {
            return _context.CST.Any(e => e.Codigo == id);
        }
    }
}
