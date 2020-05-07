using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Uni.Data;
using Uni.Models;

namespace Uni.Controllers
{
    [Authorize(Roles = "Admin, Gerente")]
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
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
                .Include(v => v.Endereco).Include(v => v.Telefone)
                .FirstOrDefaultAsync(m => m.Telefone_Id_telefone == id);

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
        public async Task<IActionResult> Create([Bind("Cnpj,Nome_empresa,Email,Inscricao_estadual,Inscricao_municipal, Endereco,Telefone")] Fornecedor fornecedor)
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
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
               .Include(v => v.Endereco).Include(v => v.Telefone)
               .FirstOrDefaultAsync(m => m.Telefone_Id_telefone == id);

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
        public async Task<IActionResult> Edit(string id, [Bind("Cnpj,Nome_empresa,Email,Inscricao_estadual,Inscricao_municipal, Endereco_Id_endereco, Telefone_Id_telefone, Telefone, Endereco")] Fornecedor fornecedor)
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
                    fornecedores.Inscricao_estadual = fornecedor.Inscricao_estadual;
                    fornecedores.Inscricao_municipal = fornecedor.Inscricao_municipal;
                    fornecedores.Nome_empresa = fornecedor.Nome_empresa;

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedor
               .Include(v => v.Endereco).Include(v => v.Telefone)
               .FirstOrDefaultAsync(m => m.Telefone_Id_telefone == id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int endereco)
        {
            var fornecedor = await _context.Fornecedor
               .Include(v => v.Endereco).Include(v => v.Telefone)
               .FirstOrDefaultAsync(m => m.Telefone_Id_telefone == id);

            var telefone = await _context.Telefone.FindAsync(id);
            var enderecos = await _context.Endereco.FindAsync(endereco);

            _context.Fornecedor.Remove(fornecedor);
            _context.Telefone.Remove(telefone);
            _context.Endereco.Remove(enderecos);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(string id)
        {
            return _context.Fornecedor.Any(e => e.Cnpj == id);
        }
    }
}
