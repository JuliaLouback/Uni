using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Uni.Models;

namespace Uni.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
        
            string[] array = new string[4] { "Admin", "Gerente", "RH", "Vendedor" };

            foreach (var thing in array)
            {
                await DoAsync(thing);
            }

            return View();
        }

        private async Task DoAsync(string thing)
        {
            var roleExiste = await _roleManager.RoleExistsAsync(thing);
            if (!roleExiste)
            {

                await _roleManager.CreateAsync(new IdentityRole { Name = thing });
            }

            if (thing == "Admin")
            {
                var user = new IdentityUser { UserName = "admin@gmail.com", Email = "admin@gmail.com" };
                await _userManager.CreateAsync(user, "1234Ju#");

                var applicationRole = await _roleManager.FindByNameAsync(thing);
                if (applicationRole != null)
                {
                    await _userManager.AddToRoleAsync(user, applicationRole.Name);
                }
            }
        }
        
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
