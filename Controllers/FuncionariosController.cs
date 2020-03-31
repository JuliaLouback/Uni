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
    public class FuncionariosController : Controller
    {
        private readonly UniContext _context;

        public FuncionariosController(UniContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            var uniContext = _context.Funcionario.Include(f => f.Endereco).Include(f => f.Telefone);
            return View(await uniContext.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .Include(f => f.Endereco)
                .Include(f => f.Telefone)
                .FirstOrDefaultAsync(m => m.Cpf == id);

            var telefone1 = await _context.Telefone.FindAsync(funcionario.Telefone_Id_telefone);
            var endereco1 = await _context.Endereco.FindAsync(funcionario.Endereco_Id_endereco);

            funcionario.Telefone = telefone1;
            funcionario.Endereco = endereco1;

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["Endereco_Id_endereco"] = new SelectList(_context.Endereco, "Id_endereco", "Id_endereco");
            ViewData["Telefone_Id_telefone"] = new SelectList(_context.Telefone, "Id_telefone", "Id_telefone");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpf,Nome,Email,Cargo,Data_nascimento,Endereco_Id_endereco,Telefone_Id_telefone, Telefone, Endereco")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                Telefone telefone = new Telefone();
                telefone.Telefones = funcionario.Telefone.Telefones;

                Endereco endereco = new Endereco();
                endereco.Cep = funcionario.Endereco.Cep;
                endereco.Rua = funcionario.Endereco.Rua;
                endereco.Numero = funcionario.Endereco.Numero;
                endereco.Bairro = funcionario.Endereco.Bairro;
                endereco.Cidade = funcionario.Endereco.Cidade;
                endereco.Estado = funcionario.Endereco.Estado;

                int lastestTelefoneId = telefone.Id_telefone;
                int lastestEnderecoId = endereco.Id_endereco;

                funcionario.Telefone_Id_telefone = lastestTelefoneId;
                funcionario.Endereco_Id_endereco = lastestEnderecoId;

                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(long? id, int telefone, int endereco)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            var telefone1 = await _context.Telefone.FindAsync(telefone);
            var endereco1 = await _context.Endereco.FindAsync(endereco);

            funcionario.Telefone = telefone1;
            funcionario.Endereco = endereco1;

            if (funcionario == null)
            {
                return NotFound();
            }
            
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Cpf,Nome,Email,Cargo,Data_nascimento,Endereco_Id_endereco,Telefone_Id_telefone, Telefone, Endereco")] Funcionario funcionario)
        {
            if (id != funcionario.Cpf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   var telefones = _context.Telefone.First(a => a.Id_telefone == funcionario.Telefone_Id_telefone);
                    telefones.Telefones = funcionario.Telefone.Telefones;

                    var funcionarios = _context.Funcionario.First(a => a.Cpf == funcionario.Cpf);
                    funcionarios.Cpf = funcionario.Cpf;
                    funcionarios.Email = funcionario.Email;
                    funcionarios.Cargo = funcionario.Cargo;
                    funcionarios.Data_nascimento = funcionario.Data_nascimento;
                    funcionarios.Nome = funcionario.Nome;

                    var enderecos = _context.Endereco.First(a => a.Id_endereco == funcionario.Endereco_Id_endereco);
                    enderecos.Numero = funcionario.Endereco.Numero;
                    enderecos.Rua = funcionario.Endereco.Rua;
                    enderecos.Cep = funcionario.Endereco.Cep;
                    enderecos.Bairro = funcionario.Endereco.Bairro;
                    enderecos.Cidade = funcionario.Endereco.Cidade;
                    enderecos.Estado = funcionario.Endereco.Estado;

                    _context.Update(funcionarios);
                    _context.Update(telefones);
                    _context.Update(enderecos);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Cpf))
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
           
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .Include(f => f.Endereco)
                .Include(f => f.Telefone)
                .FirstOrDefaultAsync(m => m.Cpf == id);

            var telefone1 = await _context.Telefone.FindAsync(funcionario.Telefone_Id_telefone);
            var endereco1 = await _context.Endereco.FindAsync(funcionario.Endereco_Id_endereco);

            funcionario.Telefone = telefone1;
            funcionario.Endereco = endereco1;

            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);


            var telefone = await _context.Telefone.FindAsync(funcionario.Telefone_Id_telefone);

            var endereco = await _context.Endereco.FindAsync(funcionario.Endereco_Id_endereco);

            _context.Funcionario.Remove(funcionario);
            _context.Telefone.Remove(telefone);
            _context.Endereco.Remove(endereco);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(long id)
        {
            return _context.Funcionario.Any(e => e.Cpf == id);
        }
    }
}
