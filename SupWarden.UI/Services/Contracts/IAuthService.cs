using SupWarden.UI.Models;

namespace SupWarden.UI.Services.Contracts;

public interface IAuthService
{
    Task<AuthModel?> LoginAsync(LoginVM loginVM); 
}
