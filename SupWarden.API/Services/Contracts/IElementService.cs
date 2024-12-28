namespace SupWarden.API.Services.Contracts;

public interface IElementService : IBaseService<Element>
{
    Task<Element?> GetByIdWithVaultAsync(string id);

    Task<IEnumerable<Element>?> GetElementsByVaultIdAsync(string vaultId);
    Task<IEnumerable<Element>?> GetElementsByCurrentUserAsync();
}