using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
        private static string valor;
        public ProdutoesController(UniContext context)
        {
            _context = context;
        }

        //pesquisa

        public async Task<IActionResult> Index(string nome, string unidade, string cnpj, int? page)
        {
            System.Diagnostics.Debug.WriteLine(cnpj);
            ViewData["Fornecedor"] = new SelectList(_context.Fornecedor, "Cnpj", "Nome_empresa");

            var produto = from m in _context.Produto.Include(v => v.Fornecedor)
                          select m;

            if (!string.IsNullOrEmpty(nome))
            {
                produto = produto.Where(j => j.Nome.Contains(nome));
            }

            if (!string.IsNullOrEmpty(unidade))
            {
                produto = produto.Where(u => u.Unidade_medida == unidade);
            }

            if (!string.IsNullOrEmpty(cnpj))
            {
                produto = produto.Where(u => u.Fornecedor.Cnpj == cnpj);
            }

            int PageSize = 4;
            int TotalCount = produto.ToList().Count;
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
            ViewBag.Nome = nome;
            ViewBag.Unidade_medida = unidade;
            ViewBag.Fornecedor = cnpj;

            return View(await produto.Skip((page ?? 0) * PageSize).Take(PageSize).ToListAsync());
            /*return View(await produto.ToListAsync());*/
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        } 

        // GET: Produtoes
       /* public async Task<IActionResult> Index()
        {
            var uniContext = _context.Produto.Include(p => p.Fornecedor);
            return View(await uniContext.ToListAsync());
        }*/

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

                DateTime localDate = DateTime.Now;

                Historico historico = new Historico();
                historico.Data_inicio = localDate;
                historico.Produto = produto;
                historico.Produto_Id_produto = produto.Id_produto;
                historico.Valor = produto.Valor_unitario;

                _context.Add(produto);
                _context.Historico.Add(historico);
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

            valor = produto.Valor_unitario;

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

                    if (valor != produto.Valor_unitario)
                    {
                        DateTime localDate = DateTime.Now;

                        var historicoAntigo = _context.Historico.OrderByDescending(x => x.Produto_Id_produto).FirstOrDefault();
                        historicoAntigo.Data_final = localDate;

                        Historico historico = new Historico();
                        historico.Data_inicio = localDate;
                        historico.Produto = produto;
                        historico.Produto_Id_produto = produto.Id_produto;
                        historico.Valor = produto.Valor_unitario;

                        _context.Update(historicoAntigo);
                        _context.Add(historico);
                    }
                    

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

        public async Task<ActionResult> ErroProduto(int id)
        {
            var produto = await _context.Produto.FirstOrDefaultAsync(m => m.Id_produto == id);

            ViewBag.Nome = produto.Nome;
            ViewBag.Descricao = produto.Descricao;
            ViewBag.Id = id;
            return View();
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);

            var produtoVenda = await _context.VendaProduto
           .FirstOrDefaultAsync(m => m.Produto_Id_produto == id);

            var produtoCotacao = await _context.CotacaoProduto
             .FirstOrDefaultAsync(m => m.Produto_Id_produto == id);

            if (produtoVenda != null || produtoCotacao != null)
            {
                return RedirectToAction("ErroProduto", new { id = id });
            }

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
