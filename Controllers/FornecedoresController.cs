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
    public class FornecedoresController : Controller
    {

        private readonly UniContext _context;

        public FornecedoresController(UniContext context)
        {
            _context = context;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornecedor.ToListAsync());
        }

        // GET: Fornecedores/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
                .FirstOrDefaultAsync(m => m.Cnpj == id);

            var telefone1 = await _context.Telefone.FindAsync(fornecedor.Telefone_Id_telefone) ;
            var endereco1 = await _context.Endereco.FindAsync(fornecedor.Endereco_Id_endereco);

            fornecedor.Telefone = telefone1;
            fornecedor.Endereco = endereco1;

            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fornecedores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cnpj,NomeEmpresa,Email,InscricaoEstadual,InscricaoMunicipal, Endereco,Telefone")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {

                Telefone telefone = new Telefone();
                telefone.Telefones = fornecedor.Telefone.Telefones;

                Endereco endereco = new Endereco();
                endereco.Cep = fornecedor.Endereco.Cep;
                endereco.Rua = fornecedor.Endereco.Rua;
                endereco.Numero = fornecedor.Endereco.Numero;
                endereco.Bairro = fornecedor.Endereco.Bairro;
                endereco.Cidade = fornecedor.Endereco.Cidade;
                endereco.Estado = fornecedor.Endereco.Estado;

                int lastestTelefoneId = telefone.Id_telefone;
                int lastestEnderecoId = endereco.Id_endereco;

                fornecedor.Telefone_Id_telefone = lastestTelefoneId;
                fornecedor.Endereco_Id_endereco = lastestEnderecoId;

                _context.Add(fornecedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(long? id, int telefone, int endereco)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor.FindAsync(id);
            var telefone1 = await _context.Telefone.FindAsync(telefone);
            var endereco1 = await _context.Endereco.FindAsync(endereco);

            fornecedor.Telefone = telefone1;
            fornecedor.Endereco = endereco1;

            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Cnpj,NomeEmpresa,Email,InscricaoEstadual,InscricaoMunicipal, Endereco_Id_endereco, Telefone_Id_telefone, Telefone, Endereco")] Fornecedor fornecedor)
        {
            if (id != fornecedor.Cnpj)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var telefones = _context.Telefone.First(a => a.Id_telefone == fornecedor.Telefone_Id_telefone);
                    telefones.Telefones = fornecedor.Telefone.Telefones;

                    var fornecedores = _context.Fornecedor.First(a => a.Cnpj == fornecedor.Cnpj);
                    fornecedores.Cnpj = fornecedor.Cnpj;
                    fornecedores.Email = fornecedor.Email;
                    fornecedores.InscricaoEstadual = fornecedor.InscricaoEstadual;
                    fornecedores.InscricaoMunicipal = fornecedor.InscricaoMunicipal;
                    fornecedores.NomeEmpresa = fornecedor.NomeEmpresa;

                    var enderecos = _context.Endereco.First(a => a.Id_endereco == fornecedor.Endereco_Id_endereco);
                    enderecos.Numero = fornecedor.Endereco.Numero;
                    enderecos.Rua = fornecedor.Endereco.Rua;
                    enderecos.Cep = fornecedor.Endereco.Cep;
                    enderecos.Bairro = fornecedor.Endereco.Bairro;
                    enderecos.Cidade = fornecedor.Endereco.Cidade;
                    enderecos.Estado = fornecedor.Endereco.Estado;

                    _context.Update(fornecedores);
                    _context.Update(telefones);
                    _context.Update(enderecos);

                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Cnpj))
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
            return View(fornecedor);
        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
                .FirstOrDefaultAsync(m => m.Cnpj == id);

            var telefone1 = await _context.Telefone.FindAsync(fornecedor.Telefone_Id_telefone);
            var endereco1 = await _context.Endereco.FindAsync(fornecedor.Endereco_Id_endereco);

            fornecedor.Telefone = telefone1;
            fornecedor.Endereco = endereco1;

            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var fornecedor = await _context.Fornecedor.FindAsync(id);

            var telefone = await _context.Telefone.FindAsync(fornecedor.Telefone_Id_telefone);

            var endereco = await _context.Endereco.FindAsync(fornecedor.Endereco_Id_endereco);

            _context.Fornecedor.Remove(fornecedor);
            _context.Telefone.Remove(telefone);
            _context.Endereco.Remove(endereco);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(long id)
        {
            return _context.Fornecedor.Any(e => e.Cnpj == id);
        }
    }
}
