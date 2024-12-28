using Newtonsoft.Json;
using SupWarden.Dto.Dtos.User;
using SupWarden.Models.Models;
using SupWarden.Ressource.Ressources.UserRessources.Update;
using SupWarden.UI.Models;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.User;
using System.Text;

namespace SupWarden.UI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApiSecurity _apiSecurity;

        public UserService(ApiSecurity apiSecurity)
        {
            this._apiSecurity = apiSecurity;
        }

        public async Task<IEnumerable<UserDto>?> GetAllUsersAsync()
        {
            var response = await _apiSecurity.Http.GetAsync("User/GetAll");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserDto>>();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<UserDto?> GetUserByIdAsync(string id)
        {
            var response = await _apiSecurity.Http.GetAsync($"User/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        // Ajouter un utilisateur
        public async Task<bool> AddUserAsync(CreateUserVM createUserVM)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(createUserVM), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("User/Create", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }



        public async Task<bool> UpdateUserAsync(string userId, UserDto userDto)
        {
            // Conversion du modèle en JSON
            var jsonContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json");

            // Concaténation de l'ID de l'utilisateur dans l'URL de la requête
            var response = await _apiSecurity.Http.PutAsync($"User/Update?id={userId}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var requestContent = new { UserId = userId, OldPassword = oldPassword, NewPassword = newPassword };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("User/ChangePassword", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SetPinCodeAsync(int pinCode)
        {
            var requestContent = new ChangePasswordRessource() { PinCode = pinCode };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("User/UpdatePinCode", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> VerifyPinCodeAsync(string userId, string pinCode)
        {
            var requestContent = new { UserId = userId, PinCode = pinCode };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("User/VerifyPinCode", jsonContent);
            return response.IsSuccessStatusCode;
        }

       
    }
}
