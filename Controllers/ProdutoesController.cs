using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Uni.Data;
using Uni.Models;

namespace Uni.Controllers
{
    [Authorize(Roles = "Admin, Gerente")]
    public class ProdutoesController : Controller
    {
        private readonly UniContext _context;

        public ProdutoesController(UniContext context)
        {
            _context = context;
        }

        // GET: Produtoes
        public async Task<IActionResult> Index()
        {
            var uniContext = _context.Produto.Include(p => p.Fornecedor);
            return View(await uniContext.ToListAsync());
        }

        // GET: Produtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.Fornecedor)
                .Include(p => p.CST)
                .Include(p => p.CFOP)
                .Include(p => p.NCM)
                .FirstOrDefaultAsync(m => m.Id_produto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtoes/Create
        public IActionResult Create()
        {
            ViewData["Fornecedor_Cnpj"] = new SelectList(_context.Fornecedor, "Cnpj", "Nome_empresa");
            ViewData["CFOP_Codigo"] = new SelectList(_context.CFOP, "Codigo", "FullName");
            ViewData["NCM_Codigo"] = new SelectList(_context.NCM, "Codigo", "FullName");
            ViewData["CST_Codigo"] = new SelectList(_context.CST, "Codigo", "FullName");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_produto,Nome,Valor_unitario,Unidade_medida,Descricao,Estoque_minimo,Estoque_maximo,Estoque_atual,Peso_bruto, Peso_liquido,Fornecedor_Cnpj, Fornecedor, CST_Codigo, CST, CFOP_Codigo, CFOP, NCM_Codigo, NCM")] Produto produto)
        {
            if (ModelState.IsValid)
            {

                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Cnpj = produto.Fornecedor_Cnpj;

                CST cst = new CST();
                cst.Codigo = produto.CST_Codigo;

                CFOP cfop = new CFOP();
                cfop.Codigo = produto.CFOP_Codigo;

                NCM ncm = new NCM();
                ncm.Codigo = produto.NCM_Codigo;

                string lastestFornecedorId = fornecedor.Cnpj;
                produto.Fornecedor_Cnpj = lastestFornecedorId;

                string lastestCSTId = cst.Codigo;
                produto.CST_Codigo = lastestCSTId;

                long lastestCFOPId = cfop.Codigo;
                produto.CFOP_Codigo = lastestCFOPId;

                long lastestNCMId = ncm.Codigo;
                produto.NCM_Codigo = lastestNCMId;


                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Fornecedor_Cnpj"] = new SelectList(_context.Fornecedor, "Cnpj", "NomeEmpresa", produto.Fornecedor_Cnpj);
            ViewData["CFOP_Codigo"] = new SelectList(_context.CFOP, "Codigo", "FullName");
            ViewData["NCM_Codigo"] = new SelectList(_context.NCM, "Codigo", "FullName");
            ViewData["CST_Codigo"] = new SelectList(_context.CST, "Codigo", "FullName");
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["Fornecedor_Cnpj"] = new SelectList(_context.Fornecedor, "Cnpj", "Nome_empresa", produto.Fornecedor_Cnpj);
            ViewData["CFOP_Codigo"] = new SelectList(_context.CFOP, "Codigo", "FullName");
            ViewData["NCM_Codigo"] = new SelectList(_context.NCM, "Codigo", "FullName");
            ViewData["CST_Codigo"] = new SelectList(_context.CST, "Codigo", "FullName");
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_produto,Nome,Valor_unitario,Unidade_medida,Descricao,Estoque_minimo,Estoque_maximo,Estoque_atual,Peso_bruto, Peso_liquido, Fornecedor_Cnpj, CST_Codigo, CFOP_Codigo, NCM_Codigo")] Produto produto)
        {
            if (id != produto.Id_produto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id_produto))
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
            ViewData["Fornecedor_Cnpj"] = new SelectList(_context.Fornecedor, "Cnpj", "Nome_empresa", produto.Fornecedor_Cnpj);
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id_produto == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id_produto == id);
        }
    }
}
