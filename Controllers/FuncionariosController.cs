using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Uni.Data;
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
        private static string cargos;
        private static string emails;
        
        
        public FuncionariosController(UniContext context, UserManager<IdentityUser> userManager, IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            var uniContext = _context.Funcionario.Include(f => f.Endereco).Include(f => f.Telefone);
            return View(await uniContext.ToListAsync());
        }

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
        public async Task<IActionResult> Create([Bind("Cpf,Nome,Email,Cargo,Data_nascimento,Endereco_Id_endereco,Senha,Status,Telefone_Id_telefone, Telefone, Endereco")] Funcionario funcionario)
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

                var user = new IdentityUser { UserName = funcionario.Email, Email = funcionario.Email };
                var result = await _userManager.CreateAsync(user, funcionario.Senha);
                var applicationRole = await _roleManager.FindByNameAsync(funcionario.Cargo);
                if (applicationRole != null)
                {
                    
                    IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                }

                _context.Add(funcionario);
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
        public async Task<IActionResult> Edit(string? id, int telefone, int endereco, string cargo, string email)
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
            cargos = cargo;
      
            emails = email;
            System.Diagnostics.Debug.WriteLine(emails);

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
        public async Task<IActionResult> Edit(string id, [Bind("Cpf,Nome,Email,Senha,Cargo,Data_nascimento,Status,Endereco_Id_endereco,Telefone_Id_telefone, Telefone, Endereco")] Funcionario funcionario)
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

                    if (funcionario.Status == "Inativo")
                    {
                        var user = await _userManager.FindByNameAsync(funcionario.Email);
                        var applicationRole = await _roleManager.FindByNameAsync(funcionario.Cargo);
                        await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);
                        await _userManager.DeleteAsync(user);
                    }
                    else
                    {
                        var user = new IdentityUser { UserName = funcionario.Email, Email = funcionario.Email };

                        if (user == null)
                        {
                            var result = await _userManager.CreateAsync(user, funcionario.Senha);
                            var applicationRole = await _roleManager.FindByNameAsync(funcionario.Cargo);
                            if (applicationRole != null)
                            {

                                IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                            }
                        }

                    }

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

                    await MudarRole(funcionario.Email, funcionario.Cargo);
                    SendEmail(funcionario.Email, null);
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

        public async Task MudarRole(string email, string cargo)
        {
            if (emails != email)
            {
                var user = await _userManager.FindByEmailAsync(emails);
                user.Email = email;
                user.UserName = email;

                await _userManager.UpdateAsync(user);


                if (cargos != cargo)
                {
                    var applicationRole = await _roleManager.FindByNameAsync(cargos);
                    await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);

                    var applicationRole1 = await _roleManager.FindByNameAsync(cargo);
                    if (applicationRole1 != null)
                    {
                        await _userManager.AddToRoleAsync(user, applicationRole1.Name);
                    }

                }
                
            }
            else if (cargos != cargo)
            {
                var user = await _userManager.FindByNameAsync(emails);

                if (user != null)
                {
                    var applicationRole = await _roleManager.FindByNameAsync(cargos);
                    await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);

                    var applicationRole1 = await _roleManager.FindByNameAsync(cargo);
                    if (applicationRole1 != null)
                    {
                       await _userManager.AddToRoleAsync(user, applicationRole1.Name);
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

            var funcionarioVenda = await _context.Venda
            .FirstOrDefaultAsync(m => m.Funcionario_Cpf == id);

            var funcionarioCotacao = await _context.Cotacao
             .FirstOrDefaultAsync(m => m.Funcionario_Cpf == id);

            if (funcionarioVenda != null || funcionarioCotacao != null)
            {
                return RedirectToAction("ErroFuncionario", new { id = id });
            }

            var telefone = await _context.Telefone.FindAsync(funcionario.Telefone_Id_telefone);

            var endereco = await _context.Endereco.FindAsync(funcionario.Endereco_Id_endereco);

            _context.Funcionario.Remove(funcionario);
            _context.Telefone.Remove(telefone);
            _context.Endereco.Remove(endereco);

            await _context.SaveChangesAsync();

            var user = await _userManager.FindByNameAsync(funcionario.Email);
            var applicationRole = await _roleManager.FindByNameAsync(funcionario.Cargo);
            await _userManager.RemoveFromRoleAsync(user, applicationRole.Name);
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(string id)
        {
            return _context.Funcionario.Any(e => e.Cpf == id);
        }
    }
}
