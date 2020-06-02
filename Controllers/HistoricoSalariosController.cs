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
    public class HistoricoSalariosController : Controller
    {
        private readonly UniContext _context;

        public HistoricoSalariosController(UniContext context)
        {
            _context = context;
        }

        // GET: HistoricoSalarios
        public async Task<IActionResult> Index(string dataIni, string dataFin, string funcionario, string cargo, int? page)
        {
            ViewData["Funcionario"] = new SelectList(_context.Funcionario, "Cpf", "Nome");

            var historico = from m in _context.HistoricoSalario.Include(v => v.Funcionario).OrderByDescending(x => x.Id_historicoSalario)
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


            if (!string.IsNullOrEmpty(cargo))
            {
                historico = historico.Where(s => s.Cargo == cargo);
            }

            /*return View(await historico.ToListAsync());*/

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
            ViewBag.Cargo = cargo;

            return View(await historico.Skip((page ?? 0) * PageSize).Take(PageSize).ToListAsync());
        }

        // GET: HistoricoSalarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoSalario = await _context.HistoricoSalario
                .Include(h => h.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_historicoSalario == id);
            if (historicoSalario == null)
            {
                return NotFound();
            }

            return View(historicoSalario);
        }

        // GET: HistoricoSalarios/Create
        public IActionResult Create()
        {
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cpf");
            return View();
        }

        // POST: HistoricoSalarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_historicoSalario,Data_inicio,Data_final,Funcionario_Cpf,Salario,Cargo")] HistoricoSalario historicoSalario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicoSalario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cpf", historicoSalario.Funcionario_Cpf);
            return View(historicoSalario);
        }

        // GET: HistoricoSalarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoSalario = await _context.HistoricoSalario.FindAsync(id);
            if (historicoSalario == null)
            {
                return NotFound();
            }
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cpf", historicoSalario.Funcionario_Cpf);
            return View(historicoSalario);
        }

        // POST: HistoricoSalarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_historicoSalario,Data_inicio,Data_final,Funcionario_Cpf,Salario,Cargo")] HistoricoSalario historicoSalario)
        {
            if (id != historicoSalario.Id_historicoSalario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicoSalario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoSalarioExists(historicoSalario.Id_historicoSalario))
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
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cpf", historicoSalario.Funcionario_Cpf);
            return View(historicoSalario);
        }

        // GET: HistoricoSalarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoSalario = await _context.HistoricoSalario
                .Include(h => h.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_historicoSalario == id);
            if (historicoSalario == null)
            {
                return NotFound();
            }

            return View(historicoSalario);
        }

        // POST: HistoricoSalarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historicoSalario = await _context.HistoricoSalario.FindAsync(id);
            _context.HistoricoSalario.Remove(historicoSalario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoSalarioExists(int id)
        {
            return _context.HistoricoSalario.Any(e => e.Id_historicoSalario == id);
        }
    }
}
