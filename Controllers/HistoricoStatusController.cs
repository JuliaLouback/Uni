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
    public class HistoricoStatusController : Controller
    {
        private readonly UniContext _context;

        public HistoricoStatusController(UniContext context)
        {
            _context = context;
        }

        // GET: HistoricoStatus
        public async Task<IActionResult> Index(string dataIni, string dataFin, string funcionario, int? page)
        {
            ViewData["Funcionario"] = new SelectList(_context.Funcionario, "Cpf", "Nome");

            var historico = from m in _context.HistoricoStatus.Include(v => v.Funcionario).OrderByDescending(x => x.Id_historicoStatus)
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
                historico = historico.Where(s => s.Data_inicio.Value.Date >= date && (s.Data_inicio.Value.Date < nextDay || s.Data_inicio.Value.Date == null));
            }

            if (!string.IsNullOrEmpty(funcionario))
            {
                historico = historico.Where(s => s.Funcionario_Cpf == funcionario);
            }

            int PageSize = 4;
            int TotalCount = historico.ToList().Count;
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
            ViewBag.Data_venda = dataIni;
            ViewBag.Data_venda = dataFin;
            ViewBag.Funcionario = funcionario;

            return View(await historico.Skip((page ?? 0) * PageSize).Take(PageSize).ToListAsync());

            /*return View(await historico.ToListAsync());*/
        }

        // GET: HistoricoStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoStatus = await _context.HistoricoStatus
                .Include(h => h.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_historicoStatus == id);
            if (historicoStatus == null)
            {
                return NotFound();
            }

            return View(historicoStatus);
        }

        // GET: HistoricoStatus/Create
        public IActionResult Create()
        {
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cpf");
            return View();
        }

        // POST: HistoricoStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_historicoStatus,Data_inicio,Data_final,Funcionario_Cpf,Status")] HistoricoStatus historicoStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicoStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cpf", historicoStatus.Funcionario_Cpf);
            return View(historicoStatus);
        }

        // GET: HistoricoStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoStatus = await _context.HistoricoStatus.FindAsync(id);
            if (historicoStatus == null)
            {
                return NotFound();
            }
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cpf", historicoStatus.Funcionario_Cpf);
            return View(historicoStatus);
        }

        // POST: HistoricoStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_historicoStatus,Data_inicio,Data_final,Funcionario_Cpf,Status")] HistoricoStatus historicoStatus)
        {
            if (id != historicoStatus.Id_historicoStatus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicoStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoStatusExists(historicoStatus.Id_historicoStatus))
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
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cpf", historicoStatus.Funcionario_Cpf);
            return View(historicoStatus);
        }

        // GET: HistoricoStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoStatus = await _context.HistoricoStatus
                .Include(h => h.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_historicoStatus == id);
            if (historicoStatus == null)
            {
                return NotFound();
            }

            return View(historicoStatus);
        }

        // POST: HistoricoStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historicoStatus = await _context.HistoricoStatus.FindAsync(id);
            _context.HistoricoStatus.Remove(historicoStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoStatusExists(int id)
        {
            return _context.HistoricoStatus.Any(e => e.Id_historicoStatus == id);
        }
    }
}
