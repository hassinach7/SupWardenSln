using SupWarden.Dto.Dtos.Element;
using SupWarden.Dto.Dtos.Vault;

namespace SupWarden.API.Services.Contracts;

public interface IVaultService : IBaseService<Vault>
{
    Task<IEnumerable<Vault?>> GetVaultsAsync();
    Task<Vault?> GetBydIdIncludeShareAndElementsAsync(string id);
    Task<IEnumerable<VaultDto?>> GetSharedVaultsAsync();
    Task<(IEnumerable<Element?>, PermissionLevel)> GetSharedElementsByVaultIdAsync(string vaultId);
}