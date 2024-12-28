
using SupWarden.API.Helpers;
using SupWarden.Models.Models;

namespace SupWarden.API.Services.Implementations;

public class ElementService : BaseService<Element>, IElementService
{
    private readonly Helper _helper;

    public ElementService(SupWardenDbContext dbContext, Helper  helper) : base(dbContext)
    {
        this._helper = helper;
    }

    public async Task<Element?> GetByIdWithVaultAsync(string id)
    {
        return await _dbContext.Elements.Include(e => e.Vault).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Element>?> GetElementsByCurrentUserAsync()
    {
        return await _dbContext.Elements.Include(o => o.Vault).Where(e => e.Vault!.UserId == _helper.GetUserId()).ToListAsync();
    }

    public async Task<IEnumerable<Element>?> GetElementsByVaultIdAsync(string vaultId)
    {
        return await _dbContext.Elements.Where(e => e.VaultId == vaultId).ToListAsync();
    }
}