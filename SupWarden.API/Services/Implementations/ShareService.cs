namespace SupWarden.API.Services.Implementations;

public class ShareService : BaseService<Share>, IShareService
{
    public ShareService(SupWardenDbContext dbContext) : base(dbContext)
    {
       
    }

    public async Task<Share?> GetShareByUserAndVaultIdAsync(string userId, string vaultId)
    {
        return await _dbContext.Shares.SingleOrDefaultAsync(o => o.UserId == userId && o.VaultId == vaultId);
    }

    public async Task<IEnumerable<Share>> GetShareListIncludeVaultAndUserAsync(string id)
    {
        return await _dbContext.Shares.Include(o => o.User).Include(o => o.Vault).Where(o => o.UserId == id).ToListAsync();
    }
}