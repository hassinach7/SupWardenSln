using Newtonsoft.Json;
using SupWarden.Dto.Dtos.Share;
using SupWarden.UI.Models;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.Share;
using System.Text;

namespace SupWarden.UI.Services.Implementations
{
    public class ShareService : IShareService
    {
        private readonly ApiSecurity _apiSecurity;

        public ShareService(ApiSecurity apiSecurity)
        {
            _apiSecurity = apiSecurity;
        }

        // Inviter un membre à un vault
        public async Task<bool> InviteMemberToVaultAsync(AddMemberToVaultVM memberVM)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(memberVM), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("Share/Create", jsonContent);
            return response.IsSuccessStatusCode;
        }

        // Récupérer les vaults partagés avec un utilisateur
        public async Task<IEnumerable<ShareDto>?> GetSharedVaultsAsync()
        {
            var response = await _apiSecurity.Http.GetAsync("Share/GetAll");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ShareDto>>();
            }
            else
            {
                throw new Exception("Unable to retrieve shared vaults.");
            }
        }

        // Mettre à jour les permissions d'un membre sur un vault
        public async Task<bool> UpdateShareAsync(UpdateShareVM shareVM)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(shareVM), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PutAsync("Share/Update", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteShareAsync(DeleteShareVM shareVM)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "Share/Delete")
            {
                Content = new StringContent(JsonConvert.SerializeObject(shareVM), Encoding.UTF8, "application/json")
            };

            var response = await _apiSecurity.Http.SendAsync(request);
            return response.IsSuccessStatusCode;
        }


    }
}
