using SupWarden.API.Helpers;

namespace SupWarden.API.Services.Contracts;

public interface  IAuthService
{
    Task<AuthModel> RegisterAsync(RegisterModel model);
    Task<AuthModel> GetTokenAsync(TokenRequestModel model);
    Task<string> AddRolesAsync(AddRolesModel model);
}