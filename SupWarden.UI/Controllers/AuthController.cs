using SupWarden.UI.Services.Contracts;

namespace SupWarden.UI.Controllers;
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        this._authService = authService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _authService.LoginAsync(model);
        if (result is not null && result.IsAuthenticated)
        {
            HttpContext.Session.SetString("JWTToken", result.Token!);
            HttpContext.Session.SetString("Email", result.Email!);
            HttpContext.Session.SetString("FullName", result.FullName!);
            HttpContext.Session.SetString("UserRoles", string.Join(",",result.Roles));
            HttpContext.Session.SetString("PinCode", result.PinCode ?? string.Empty);
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError("ApiError", result!.Messages[0].Replace("[\"","").Replace("\"]",""));
        return View(model);
    }

    [HttpPost]
    public IActionResult Logout()
    {
      
        HttpContext.Session.Clear();

      
        return RedirectToAction("Login", "Auth");
    }
    [HttpGet]
    public IActionResult ExternalLogin(string provider)
    {
        // Redirection vers l'API pour authentification externe
        return Redirect($"https://localhost:7187/api/Auth/ExternalLogin?provider={provider}");
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction(nameof(Login));
        }

        // Enregistrement du token pour utilisation future
        HttpContext.Session.SetString("JwtToken", token);

        return RedirectToAction("Index", "Home");
    }



}
