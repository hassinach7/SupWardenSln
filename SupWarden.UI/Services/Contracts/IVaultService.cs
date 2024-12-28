using SupWarden.Dto.Dtos.Element;
using SupWarden.Dto.Dtos.Vault;
using SupWarden.UI.ViewModels.Vault;

namespace SupWarden.UI.Services.Contracts
{
    public interface IVaultService
    {
        Task<IEnumerable<VaultDto>?> GetSharedVaultsAsync();
        Task<IEnumerable<VaultDto>?> GetVaultsAsync();
        Task<VaultDto?> GetVaultByIdAsync(string id);
        Task<ReturnCreatedVault?> CreateVaultAsync(CreateVaultVM vaultVM);
        Task<bool> UpdateVaultAsync(UpdateVaultVM vaultVM);
        Task<bool> DeleteVaultAsync(string vaultId);
        Task<bool> ShareAsync(ShareVaultVM request);
        Task<IEnumerable<ElementDto>?> GetSharedEelementsByVaultIdAsync(string vaultId);
    }
}
