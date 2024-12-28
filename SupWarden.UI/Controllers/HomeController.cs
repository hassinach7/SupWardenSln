using Microsoft.AspNetCore.Mvc;
using SupWarden.UI.Services.Contracts;
using SupWarden.Dto.Dtos.Vault;
using System.Diagnostics;
using System.Threading.Tasks;
using SupWarden.UI.Models;

namespace SupWarden.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVaultService _vaultService;

        public HomeController(ILogger<HomeController> logger, IVaultService vaultService)
        {
            _logger = logger;
            _vaultService = vaultService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("JWTToken")))
            {
                return RedirectToAction("login","auth");
            }
            // Récupérer la liste des Vaults
            var vaults = await _vaultService.GetVaultsAsync();
            return View(vaults);
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
