using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupWarden.API.Helpers;
using System.Security.Claims;

namespace SupWarden.API.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthController(IAuthService authService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        this._authService = authService;
        this._userManager = userManager;
        this._roleManager = roleManager;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync(TokenRequestModel model)
    {

        var searchUser = await _userManager.FindByEmailAsync("admin@supwarden.fr");
        if (searchUser is null)
        {
            var user = new User
            {
                Email = "admin@supwarden.fr",
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@supwarden.fr",
                PhoneNumber = "0900000000"
            };

            await _userManager.CreateAsync(user, "Password1!");
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");

            }
        }
        else
        {
            if (!await _userManager.IsInRoleAsync(searchUser, "Admin"))
            {
                await _userManager.AddToRoleAsync(searchUser, "Admin");

            }
        }

        var result = await _authService.GetTokenAsync(model);
        if (!result.IsAuthenticated)
        {
            return BadRequest(result.Messages);
        }
        return Ok(result);
    }
    [HttpGet("ExternalLogin")]
    public IActionResult ExternalLogin(string provider)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Auth");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, provider);
    }

    [HttpGet("ExternalLoginCallback")]
    public async Task<IActionResult> ExternalLoginCallback()
    {
        var result = await HttpContext.AuthenticateAsync();
        if (!result.Succeeded)
            return Unauthorized();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, result.Principal.FindFirstValue(ClaimTypes.Name)),
            new Claim(ClaimTypes.Email, result.Principal.FindFirstValue(ClaimTypes.Email))
        };

        var token = "";
        return Ok(new { token });
    }
}
