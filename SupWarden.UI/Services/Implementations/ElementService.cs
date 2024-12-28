using Newtonsoft.Json;
using SupWarden.Dto.Dtos.Element;
using SupWarden.UI.Models;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.Element;
using SupWarden.UI.ViewModels.Vault;
using System.Text;

namespace SupWarden.UI.Services.Implementations
{
    public class ElementService : IElementService
    {
        private readonly ApiSecurity _apiSecurity;

        public ElementService(ApiSecurity apiSecurity)
        {
            this._apiSecurity = apiSecurity;
        }

        public async Task<IEnumerable<ElementDto>?> GetElementsAsync()
        {
            var response = await _apiSecurity.Http.GetAsync("element/GetAll");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ElementDto>>();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<ReturnCreatedElementVM?> CreateElementAsync(CreateElementVaultVM elementVM)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(elementVM), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PostAsync("element/Create", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReturnCreatedElementVM>();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<ReturnUpdatedElementVM?> UpdateElementAsync(UpdateElementVM elementVM)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(elementVM), Encoding.UTF8, "application/json");
            var response = await _apiSecurity.Http.PutAsync("element/Update", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ReturnUpdatedElementVM>();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<bool> DeleteElementAsync(string elementId)
        {
            var response = await _apiSecurity.Http.DeleteAsync($"Element/Delete?id={elementId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async ValueTask<ElementDto> GetElementByIdAsync(string id)
        {
            var response = await _apiSecurity.Http.GetAsync($"Element/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ElementDto>();
            }
            else
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
