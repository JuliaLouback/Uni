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
    public class ClientesController : Controller
    {
        private readonly UniContext _context;

        public ClientesController(UniContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var uniContext = _context.Cliente.Include(c => c.Endereco).Include(c => c.Telefone);
            return View(await uniContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Endereco)
                .Include(c => c.Telefone)
                .FirstOrDefaultAsync(m => m.Cpf == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["Endereco_Id_endereco"] = new SelectList(_context.Endereco, "Id_endereco", "Id_endereco");
            ViewData["Telefone_Id_telefone"] = new SelectList(_context.Telefone, "Id_telefone", "Id_telefone");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpf,Nome,Email,Endereco_Id_endereco,Telefone_Id_telefone, Endereco, Telefone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {

                Telefone telefone = new Telefone();
                telefone.Telefones = cliente.Telefone.Telefones;

                Endereco endereco = new Endereco();
                endereco.Cep = cliente.Endereco.Cep;
                endereco.Rua = cliente.Endereco.Rua;
                endereco.Numero = cliente.Endereco.Numero;
                endereco.Bairro = cliente.Endereco.Bairro;
                endereco.Cidade = cliente.Endereco.Cidade;
                endereco.Estado = cliente.Endereco.Estado;

                int lastestTelefoneId = telefone.Id_telefone;
                int lastestEnderecoId = endereco.Id_endereco;

                cliente.Telefone_Id_telefone = lastestTelefoneId;
                cliente.Endereco_Id_endereco = lastestEnderecoId;

                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(long? id, int telefone, int endereco)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            var telefone1 = await _context.Telefone.FindAsync(telefone);
            var endereco1 = await _context.Endereco.FindAsync(endereco);

            cliente.Telefone = telefone1;
            cliente.Endereco = endereco1;

            if (cliente == null)
            {
                return NotFound();
            }
         
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Cpf,Nome,Email,Endereco_Id_endereco,Telefone_Id_telefone, Endereco, Telefone")] Cliente cliente)
        {
            if (id != cliente.Cpf)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var telefones = _context.Telefone.First(a => a.Id_telefone == cliente.Telefone_Id_telefone);
                    telefones.Telefones = cliente.Telefone.Telefones;

                    var clientes = _context.Cliente.First(a => a.Cpf == cliente.Cpf);
                    clientes.Cpf = cliente.Cpf;
                    clientes.Email = cliente.Email;
                    clientes.Nome = cliente.Nome;

                    var enderecos = _context.Endereco.First(a => a.Id_endereco == cliente.Endereco_Id_endereco);
                    enderecos.Numero = cliente.Endereco.Numero;
                    enderecos.Rua = cliente.Endereco.Rua;
                    enderecos.Cep = cliente.Endereco.Cep;
                    enderecos.Bairro = cliente.Endereco.Bairro;
                    enderecos.Cidade = cliente.Endereco.Cidade;
                    enderecos.Estado = cliente.Endereco.Estado;

                    _context.Update(clientes);
                    _context.Update(telefones);
                    _context.Update(enderecos);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Cpf))
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
           
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Endereco)
                .Include(c => c.Telefone)
                .FirstOrDefaultAsync(m => m.Cpf == id);

            var telefone1 = await _context.Telefone.FindAsync(cliente.Telefone_Id_telefone);
            var endereco1 = await _context.Endereco.FindAsync(cliente.Endereco_Id_endereco);

            cliente.Telefone = telefone1;
            cliente.Endereco = endereco1;

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            var telefone = await _context.Telefone.FindAsync(cliente.Telefone_Id_telefone);

            var endereco = await _context.Endereco.FindAsync(cliente.Endereco_Id_endereco);

            _context.Cliente.Remove(cliente);
            _context.Telefone.Remove(telefone);
            _context.Endereco.Remove(endereco);

 
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(long id)
        {
            return _context.Cliente.Any(e => e.Cpf == id);
        }
    }
}
