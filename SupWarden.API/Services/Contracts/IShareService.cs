namespace SupWarden.API.Services.Contracts;

public interface IShareService : IBaseService<Share>
{
    Task<IEnumerable<Share>> GetShareListIncludeVaultAndUserAsync(string id);
    Task<Share?> GetShareByUserAndVaultIdAsync(string userId , string vaultId);
}