using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Uni.Data;
using Uni.Migrations;
using Uni.Models;

namespace Uni.Controllers
{
    [Authorize(Roles = "Admin, Gerente, RH")]
    public class FuncionariosController : Controller
    {
        private readonly UniContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private static Funcionario funcionarioAdd = new Funcionario();
           
        public FuncionariosController(UniContext context, UserManager<IdentityUser> userManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index(string searchString, string searchString2, string searchString3, string searchString4)
        {
            var funcionarios = from m in _context.Funcionario.Include(v => v.Telefone)
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                funcionarios = funcionarios.Where(s => s.Nome.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchString2))
            {
                funcionarios = funcionarios.Where(y => y.Cpf.Contains(searchString2));
            }
            

            if (!string.IsNullOrEmpty(searchString3))
            {
                funcionarios = funcionarios.Where(x => x.Status == searchString3);
            }

            if (!string.IsNullOrEmpty(searchString4))
            {
                funcionarios = funcionarios.Where(t => t.Cargo == searchString4);
            }
            return View(await funcionarios.ToListAsync());
        }
        
        

        /*
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        */
       
        /* public async Task<IActionResult> Index()
        {
            var uniContext = _context.Funcionario.Include(f => f.Endereco).Include(f => f.Telefone);
            return View(await uniContext.ToListAsync());
        }

        */

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(string? id)
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
        public async Task<IActionResult> Create([Bind("Cpf,Nome,Email,Cargo,Data_nascimento,Endereco_Id_endereco,Senha,Status,Telefone_Id_telefone,Salario, Telefone, Endereco")] Funcionario funcionario)
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

                DateTime localDate = DateTime.Now;

                HistoricoStatus historicoStatus = new HistoricoStatus();
                historicoStatus.Data_inicio = localDate;
                historicoStatus.Funcionario = funcionario;
                historicoStatus.Funcionario_Cpf = funcionario.Cpf;
                historicoStatus.Status = funcionario.Status;

                HistoricoSalario historicoSalario = new HistoricoSalario();
                historicoSalario.Data_inicio = localDate;
                historicoSalario.Funcionario = funcionario;
                historicoSalario.Funcionario_Cpf = funcionario.Cpf;
                historicoSalario.Cargo = funcionario.Cargo;
                historicoSalario.Salario = funcionario.Salario;

                int lastestTelefoneId = telefone.Id_telefone;
                int lastestEnderecoId = endereco.Id_endereco;

                funcionario.Telefone_Id_telefone = lastestTelefoneId;
                funcionario.Endereco_Id_endereco = lastestEnderecoId;

                var user = new IdentityUser { UserName = funcionario.Email, Email = funcionario.Email };
                var result = await _userManager.CreateAsync(user, funcionario.Senha);
                var applicationRole = await _roleManager.FindByNameAsync(funcionario.Cargo);
                if (applicationRole != null)
                {
                    
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                }

                _context.Add(funcionario);
                _context.Add(historicoStatus);
                _context.Add(historicoSalario);
                await _context.SaveChangesAsync();
                SendEmail(funcionario.Email,funcionario.Senha);
                return RedirectToAction(nameof(Index));
            }

            return View(funcionario);
        }

        public void SendEmail(string Email, string Senha)
        {
            string assunto = "Cadastro Efetuado - Uni Construções";
            string mensagem;

            if (Senha != null)
            {
                mensagem = $"Seu cadastro na Empresa Uni Construções foi efetuado, utilize o e-mail: {Email} e a senha: {Senha} para realizar o login!";
            }
            else
            {
                mensagem = $"Seu cadastro na Empresa Uni Construções foi efetuado, utilize o e-mail: {Email} para realizar o login clicando em esqueci minha senha no primeiro acesso!";
            } 

            var credentials = new NetworkCredential("projetouniconstrucoes@gmail.com", "UniConstrucao1234");
            // Mail message
            var mail = new MailMessage()
            {
                From = new MailAddress("noreply@uniconstrucoes.com"),
                Subject = assunto,
                Body = mensagem
            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(Email));
            // Smtp client
            var client = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };

            client.Send(mail);
        }
        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.Include(v => v.Endereco).Include(v => v.Telefone).FirstOrDefaultAsync(x=> x.Cpf == id);
            funcionarioAdd = funcionario;

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
        public async Task<IActionResult> Edit(string id, [Bind("Cpf,Nome,Email,Senha,Cargo,Data_nascimento,Status,Endereco_Id_endereco,Telefone_Id_telefone,Salario, Telefone, Endereco")] Funcionario funcionario)
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
                    funcionarios.Senha = funcionario.Senha;
                    funcionarios.Status = funcionario.Status;
                    funcionarios.Salario = funcionario.Salario;

                    var enderecos = _context.Endereco.First(a => a.Id_endereco == funcionario.Endereco_Id_endereco);
                    enderecos.Numero = funcionario.Endereco.Numero;
                    enderecos.Rua = funcionario.Endereco.Rua;
                    enderecos.Cep = funcionario.Endereco.Cep;
                    enderecos.Bairro = funcionario.Endereco.Bairro;
                    enderecos.Cidade = funcionario.Endereco.Cidade;
                    enderecos.Estado = funcionario.Endereco.Estado;

                    if (funcionarioAdd.Status != funcionarios.Status)
                    {
                        DateTime localDate = DateTime.Now;

                        var historicoAntigo = _context.HistoricoStatus.OrderByDescending(x => x.Funcionario_Cpf).FirstOrDefault();
                        historicoAntigo.Data_final = localDate;

                        HistoricoStatus historico = new HistoricoStatus();
                        historico.Data_inicio = localDate;
                        historico.Funcionario = funcionarios;
                        historico.Funcionario_Cpf = funcionarios.Cpf;
                        historico.Status = funcionarios.Status;

                        _context.Update(historicoAntigo);
                        _context.Add(historico);
                    }

                    if (funcionarioAdd.Salario != funcionarios.Salario || funcionarioAdd.Cargo != funcionario.Cargo)
                    {
                        DateTime localDate = DateTime.Now;

                        var historicoAntigo = _context.HistoricoSalario.OrderByDescending(x => x.Funcionario_Cpf).FirstOrDefault();
                        historicoAntigo.Data_final = localDate;

                        HistoricoSalario historico = new HistoricoSalario();
                        historico.Data_inicio = localDate;
                        historico.Funcionario = funcionarios;
                        historico.Funcionario_Cpf = funcionarios.Cpf;
                        historico.Cargo = funcionarios.Cargo;
                        historico.Salario = funcionarios.Salario;

                        _context.Update(historicoAntigo);
                        _context.Add(historico);
                    }

                    _context.Update(funcionarios);
                    _context.Update(telefones);
                    _context.Update(enderecos);

                    await _context.SaveChangesAsync();

                    System.Diagnostics.Debug.WriteLine(funcionario.Senha);

                    await MudarRole(funcionario.Email, funcionario.Cargo, funcionario.Status, funcionario.Senha);
                    SendEmail(funcionario.Email, funcionario.Senha);
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

        public async Task MudarRole(string email, string cargo, string status, string senha)
        {
            var user2 = await _userManager.FindByEmailAsync(funcionarioAdd.Email);

            if (funcionarioAdd.Email != email && user2 != null)
            {
                var user = await _userManager.FindByEmailAsync(funcionarioAdd.Email);
                user.Email = email;
                user.UserName = email;

                await _userManager.UpdateAsync(user);
                string cargo1 = "";

                if (funcionarioAdd.Cargo != cargo)
                {
                    var applicationRole = await _roleManager.FindByNameAsync(funcionarioAdd.Cargo);
                    await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);

                    var applicationRole1 = await _roleManager.FindByNameAsync(cargo);
                    if (applicationRole1 != null)
                    {
                        await _userManager.AddToRoleAsync(user, applicationRole1.Name);
                    }

                    cargo1 = cargo;
                } else
                {
                    cargo1 = funcionarioAdd.Cargo;
                }

                if (funcionarioAdd.Status == "Ativo" && status == "Inativo")
                {
                    var user1 = await _userManager.FindByNameAsync(email);
                    var applicationRole = await _roleManager.FindByNameAsync(cargo1);
                    await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);
                    await _userManager.DeleteAsync(user);
                }
                else if (funcionarioAdd.Status == "Inativo" && status == "Ativo")
                {
                    var user1 = new IdentityUser { UserName = email, Email = email };

                    if (user1 == null)
                    {
                        var result = await _userManager.CreateAsync(user, senha);
                        var applicationRole = await _roleManager.FindByNameAsync(cargo1);
                        if (applicationRole != null)
                        {
                            await _userManager.AddToRoleAsync(user1, applicationRole.Name);
                        }
                    }

                }

            }
            else if (funcionarioAdd.Cargo != cargo)
            {
                var user = await _userManager.FindByNameAsync(funcionarioAdd.Email);

                if (user != null)
                {
                    var applicationRole = await _roleManager.FindByNameAsync(funcionarioAdd.Cargo);
                    await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);

                    var applicationRole1 = await _roleManager.FindByNameAsync(cargo);
                    if (applicationRole1 != null)
                    {
                       await _userManager.AddToRoleAsync(user, applicationRole1.Name);
                    }
                }
            } else
            {
                if (funcionarioAdd.Status == "Ativo" && status == "Inativo")
                {
                    var user = await _userManager.FindByNameAsync(funcionarioAdd.Email);
                    var applicationRole = await _roleManager.FindByNameAsync(funcionarioAdd.Cargo);
                    await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);
                    await _userManager.DeleteAsync(user);
                }
                else if (funcionarioAdd.Status == "Inativo" && status == "Ativo")
                {
                    var user = new IdentityUser { UserName = email, Email = email };

                    await _userManager.CreateAsync(user, senha);
                    var applicationRole = await _roleManager.FindByNameAsync(cargo);
                    if (applicationRole != null)
                    {

                        await _userManager.AddToRoleAsync(user, applicationRole.Name);
                    }
                }
            }
        }
        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(string? id)
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

        public async Task<ActionResult> ErroFuncionario(string id)
        {
            var funcionario = await _context.Funcionario.FirstOrDefaultAsync(m => m.Cpf == id);

            ViewBag.Nome = funcionario.Nome;
            ViewBag.Cpf = funcionario.Cpf;
            ViewBag.Id = id;
            return View();
        }


        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            funcionarioAdd = funcionario;
            return RedirectToAction(nameof(ConfirmarDelete));
        }

        public ActionResult ConfirmarDelete()
        {
            ViewBag.Func = funcionarioAdd.Nome + " - " + funcionarioAdd.Cpf;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmarDelete(int? teste)
        {
            var funcionario = await _context.Funcionario.FindAsync(funcionarioAdd.Cpf);

            var funcionarioVenda = await _context.Venda
            .FirstOrDefaultAsync(m => m.Funcionario_Cpf == funcionarioAdd.Cpf);

            var funcionarioCotacao = await _context.Cotacao
             .FirstOrDefaultAsync(m => m.Funcionario_Cpf == funcionarioAdd.Cpf);

            if (funcionarioVenda != null || funcionarioCotacao != null)
            {
                return RedirectToAction("ErroFuncionario", new { id = funcionarioAdd.Cpf });
            }

            var telefone = await _context.Telefone.FindAsync(funcionario.Telefone_Id_telefone);

            var endereco = await _context.Endereco.FindAsync(funcionario.Endereco_Id_endereco);

            var historico = _context.HistoricoStatus.Where(x => x.Funcionario_Cpf == funcionario.Cpf);

            var historicoSalario = _context.HistoricoSalario.Where(x => x.Funcionario_Cpf == funcionario.Cpf);

            _context.Funcionario.Remove(funcionario);
            _context.Telefone.Remove(telefone);
            _context.Endereco.Remove(endereco);
            _context.HistoricoStatus.RemoveRange(historico);
            _context.HistoricoSalario.RemoveRange(historicoSalario);

            await _context.SaveChangesAsync();

            var user = await _userManager.FindByNameAsync(funcionario.Email);
            var applicationRole = await _roleManager.FindByNameAsync(funcionario.Cargo);
            await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        private bool FuncionarioExists(string id)
        {
            return _context.Funcionario.Any(e => e.Cpf == id);
        }
    }
}
