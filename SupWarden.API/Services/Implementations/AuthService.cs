using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SupWarden.API.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SupWarden.API.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly Jwt jwt;

    public AuthService(UserManager<User> userManager,
                       RoleManager<IdentityRole> roleManager,
                       IOptions<Jwt> jwt)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.jwt = jwt.Value;
    }

    public Task<string> AddRolesAsync(AddRolesModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
    {
        var authModel = new AuthModel();
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user is null || !(await userManager.CheckPasswordAsync(user, model.Password)))
        {
            authModel.Messages = new() { $"Email or Password is Incorrect." };
        }
        else
        {
            var jwtSecurityToekn = await CreateJwtTokenAsync(user);
            authModel.IsAuthenticated = true;
            authModel.PinCode = user.PinCode;
            authModel.ExpiredOn = jwtSecurityToekn.ValidTo;
            authModel.UserName = user!.UserName;
            authModel.FullName = user!.FirstName + " " + user!.LastName.ToUpper();
            authModel.Email = user.Email!;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToekn);
            authModel.Roles = (await userManager.GetRolesAsync(user)).ToList();
        }

        return authModel;
    }

    public Task<AuthModel> RegisterAsync(RegisterModel model)
    {
        throw new NotImplementedException();
    }

    private async Task<JwtSecurityToken> CreateJwtTokenAsync(User user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role));
        }
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("uid", user.Id),
        }.Union(userClaims).Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        return new(
            audience: jwt.Audience,
            issuer: jwt.Issuer,
            claims: claims,
            expires: DateTime.Now.AddDays(jwt.DurationInDays),
            signingCredentials: signingCredentials
            );
    }
}
