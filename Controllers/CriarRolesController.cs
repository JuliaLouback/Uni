using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uni.Data;
using Uni.Models;

namespace Uni.Controllers
{
    public class CriarRolesController : Controller
    {
        private readonly UniContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;



        public CriarRolesController(UniContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        // GET: CriarRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.CriarRole.ToListAsync());
        }

        // GET: CriarRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criarRole = await _context.CriarRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criarRole == null)
            {
                return NotFound();
            }

            return View(criarRole);
        }

        // GET: CriarRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CriarRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeRole")] CriarRole criarRole)
        {
            if (ModelState.IsValid)
            {
                var roleExiste = await _roleManager.RoleExistsAsync(criarRole.NomeRole);
                if (!roleExiste)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole{Name = criarRole.NomeRole });
                }
                _context.Add(criarRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criarRole);
        }

        // GET: CriarRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criarRole = await _context.CriarRole.FindAsync(id);
            if (criarRole == null)
            {
                return NotFound();
            }
            return View(criarRole);
        }

        // POST: CriarRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeRole")] CriarRole criarRole)
        {
            if (id != criarRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criarRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriarRoleExists(criarRole.Id))
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
            return View(criarRole);
        }

        // GET: CriarRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criarRole = await _context.CriarRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criarRole == null)
            {
                return NotFound();
            }

            return View(criarRole);
        }

        // POST: CriarRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criarRole = await _context.CriarRole.FindAsync(id);
            _context.CriarRole.Remove(criarRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriarRoleExists(int id)
        {
            return _context.CriarRole.Any(e => e.Id == id);
        }
    }
}
