using Newtonsoft.Json;
using SupWarden.UI.Models;
using SupWarden.UI.Services.Contracts;
using System.Text;

namespace SupWarden.UI.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly ApiSecurity _apiSecurity;

    public AuthService(ApiSecurity apiSecurity)
    {
        this._apiSecurity = apiSecurity;
    }
    public async Task<AuthModel?> LoginAsync(LoginVM loginVM)
    {
        // Convert the object loginVM to Json
        var jsonContent = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");


        // send the Post Request
        var response = await _apiSecurity.Http.PostAsync("auth/Login", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AuthModel>();
        }

        if (response.IsSuccessStatusCode)
        {
            return  await response.Content.ReadFromJsonAsync<AuthModel>();
        }
        return new()
        {
            IsAuthenticated = false,
            Messages = new List<string>() { await response.Content.ReadAsStringAsync() }
        };
    }
}
