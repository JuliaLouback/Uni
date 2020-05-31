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
    public class HistoricoesController : Controller
    {
        private readonly UniContext _context;

        public HistoricoesController(UniContext context)
        {
            _context = context;
        }

        // GET: Historicoes
        public async Task<IActionResult> Index(string dataIni, string dataFin, int? produto)
        {
            System.Diagnostics.Debug.WriteLine(dataIni);

            ViewData["Produto"] = new SelectList(_context.Produto, "Id_produto", "Nome");


            var historico = from m in _context.Historico.Include(v => v.Produto).OrderByDescending(x => x.Id_historico)
                                  select m;

            if (!string.IsNullOrEmpty(dataIni) && !string.IsNullOrEmpty(dataFin))
            {

                
                var date = Convert.ToDateTime(dataIni);
                var date1 = Convert.ToDateTime(dataFin);
                var nextDay = date1.AddDays(1);

                historico = historico.Where(s => s.Data_inicio.Value.Date >= date && s.Data_final.Value.Date < nextDay);
            }
            else if (!string.IsNullOrEmpty(dataIni))
            {

                var date = Convert.ToDateTime(dataIni).Date;
                var nextDay = date.AddDays(1);
                historico = historico.Where(s => s.Data_inicio.Value.Date >= date && (s.Data_inicio.Value.Date < nextDay || s.Data_inicio.Value.Date == null) );
            }

            if (produto != null)
            {
                historico = historico.Where(s => s.Produto_Id_produto == produto);
            }

            return View(await historico.ToListAsync());

        }

        // GET: Historicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historico = await _context.Historico
                .Include(h => h.Produto)
                .FirstOrDefaultAsync(m => m.Id_historico == id);
            if (historico == null)
            {
                return NotFound();
            }

            return View(historico);
        }

        // GET: Historicoes/Create
        public IActionResult Create()
        {
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome");
            return View();
        }

        // POST: Historicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_historico,Data_inicio,Data_final,Produto_Id_produto")] Historico historico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome", historico.Produto_Id_produto);
            return View(historico);
        }

        // GET: Historicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historico = await _context.Historico.FindAsync(id);
            if (historico == null)
            {
                return NotFound();
            }
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome", historico.Produto_Id_produto);
            return View(historico);
        }

        // POST: Historicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_historico,Data_inicio,Data_final,Produto_Id_produto")] Historico historico)
        {
            if (id != historico.Id_historico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoExists(historico.Id_historico))
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
            ViewData["Produto_Id_produto"] = new SelectList(_context.Produto, "Id_produto", "Nome", historico.Produto_Id_produto);
            return View(historico);
        }

        // GET: Historicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historico = await _context.Historico
                .Include(h => h.Produto)
                .FirstOrDefaultAsync(m => m.Id_historico == id);
            if (historico == null)
            {
                return NotFound();
            }

            return View(historico);
        }

        // POST: Historicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historico = await _context.Historico.FindAsync(id);
            _context.Historico.Remove(historico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoExists(int id)
        {
            return _context.Historico.Any(e => e.Id_historico == id);
        }
    }
}
