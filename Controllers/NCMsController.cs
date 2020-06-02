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
    public class NCMsController : Controller
    {
        private readonly UniContext _context;

        public NCMsController(UniContext context)
        {
            _context = context;
        }

        // GET: NCMs
        public async Task<IActionResult> Index(int? page)
        {
            var teste = _context.NCM;

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

        // GET: NCMs/Details/5
        public async Task<IActionResult> Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCM = await _context.NCM
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (nCM == null)
            {
                return NotFound();
            }

            return View(nCM);
        }

        // GET: NCMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NCMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Descricao")] NCM nCM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nCM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nCM);
        }

        // GET: NCMs/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCM = await _context.NCM.FindAsync(id);
            if (nCM == null)
            {
                return NotFound();
            }
            return View(nCM);
        }

        // POST: NCMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Codigo,Descricao")] NCM NcmTeste)
        {
            if (id != NcmTeste.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(NcmTeste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NCMExists(NcmTeste.Codigo))
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
            return View(NcmTeste);
        }

        // GET: NCMs/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCM = await _context.NCM
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (nCM == null)
            {
                return NotFound();
            }

            return View(nCM);
        }

        // POST: NCMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var nCM = await _context.NCM.FindAsync(id);
            _context.NCM.Remove(nCM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NCMExists(long id)
        {
            return _context.NCM.Any(e => e.Codigo == id);
        }
    }
}
