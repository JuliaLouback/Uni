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
    public class CotacaosController : Controller
    {
        private readonly UniContext _context;

        public CotacaosController(UniContext context)
        {
            _context = context;
        }

        // GET: Cotacaos
        public async Task<IActionResult> Index()
        {
            var uniContext = _context.Cotacao.Include(c => c.Cliente).Include(c => c.Funcionario);
            return View(await uniContext.ToListAsync());
        }

        // GET: Cotacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotacao = await _context.Cotacao
                .Include(c => c.Cliente)
                .Include(c => c.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_cotacao == id);
            if (cotacao == null)
            {
                return NotFound();
            }

            return View(cotacao);
        }

        // GET: Cotacaos/Create
        public IActionResult Create()
        {
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Email");
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cargo");
            return View();
        }

        // POST: Cotacaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_cotacao,Data_venda,Valor_total,Funcionario_Cpf,Cliente_Cpf")] Cotacao cotacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Email", cotacao.Cliente_Cpf);
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cargo", cotacao.Funcionario_Cpf);
            return View(cotacao);
        }

        // GET: Cotacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotacao = await _context.Cotacao.FindAsync(id);
            if (cotacao == null)
            {
                return NotFound();
            }
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Email", cotacao.Cliente_Cpf);
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cargo", cotacao.Funcionario_Cpf);
            return View(cotacao);
        }

        // POST: Cotacaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_cotacao,Data_venda,Valor_total,Funcionario_Cpf,Cliente_Cpf")] Cotacao cotacao)
        {
            if (id != cotacao.Id_cotacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotacaoExists(cotacao.Id_cotacao))
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
            ViewData["Cliente_Cpf"] = new SelectList(_context.Cliente, "Cpf", "Email", cotacao.Cliente_Cpf);
            ViewData["Funcionario_Cpf"] = new SelectList(_context.Funcionario, "Cpf", "Cargo", cotacao.Funcionario_Cpf);
            return View(cotacao);
        }

        // GET: Cotacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotacao = await _context.Cotacao
                .Include(c => c.Cliente)
                .Include(c => c.Funcionario)
                .FirstOrDefaultAsync(m => m.Id_cotacao == id);
            if (cotacao == null)
            {
                return NotFound();
            }

            return View(cotacao);
        }

        // POST: Cotacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cotacao = await _context.Cotacao.FindAsync(id);
            _context.Cotacao.Remove(cotacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotacaoExists(int id)
        {
            return _context.Cotacao.Any(e => e.Id_cotacao == id);
        }
    }
}
