using Newtonsoft.Json;
using SupWarden.Dto.Dtos.Element;
using SupWarden.Dto.Dtos.Vault;
using SupWarden.UI.Models;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.Vault;
using System.Text;

namespace SupWarden.UI.Services.Implementations;

public class VaultService : IVaultService
{
    private readonly ApiSecurity _apiSecurity;

    public VaultService(ApiSecurity apiSecurity)
    {
        this._apiSecurity = apiSecurity;
    }
    public async Task<IEnumerable<VaultDto>?> GetVaultsAsync()
    {


        // send the Post Request
        var response = await _apiSecurity.Http.GetAsync("vault/GetAll");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<VaultDto>>();
        }
        else
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }


    public async Task<VaultDto?> GetVaultByIdAsync(string id)
    {
        var response = await _apiSecurity.Http.GetAsync($"Vault/GetById?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<VaultDto>();
        }
        throw new Exception(await response.Content.ReadAsStringAsync());
    }

    public async Task<ReturnCreatedVault?> CreateVaultAsync(CreateVaultVM vaultVM)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(vaultVM), Encoding.UTF8, "application/json");
        var response = await _apiSecurity.Http.PostAsync("vault/Create", jsonContent);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ReturnCreatedVault>();
        }
        throw new Exception(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> UpdateVaultAsync(UpdateVaultVM vaultVM)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(vaultVM), Encoding.UTF8, "application/json");
        var response = await _apiSecurity.Http.PutAsync("Vault/Update", jsonContent);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }

    }

    public async Task<bool> DeleteVaultAsync(string vaultId)
    {
        var response = await _apiSecurity.Http.DeleteAsync($"Vault/Delete?id={vaultId}");
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        throw new Exception(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> ShareAsync(ShareVaultVM request)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _apiSecurity.Http.PostAsync("Vault/Share", jsonContent);
        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task<IEnumerable<VaultDto>?> GetSharedVaultsAsync()
    {
        var response = await _apiSecurity.Http.GetAsync("vault/SharedVaults");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<VaultDto>>();
        }
        else
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task<IEnumerable<ElementDto>?> GetSharedEelementsByVaultIdAsync(string vaultId)
    {
        var response = await _apiSecurity.Http.GetAsync("vault/SharedElements?id=" + vaultId);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<IEnumerable<ElementDto>>();
        }
        else
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}