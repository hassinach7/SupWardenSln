
using SupWarden.API.Helpers;
using SupWarden.Dto.Dtos.Element;
using SupWarden.Dto.Dtos.Vault;

namespace SupWarden.API.Services.Implementations;

public class VaultService : BaseService<Vault>, IVaultService
{
    private readonly Helper _helper;

    public VaultService(SupWardenDbContext dbContext, Helper helper) : base(dbContext)
    {
        this._helper = helper;
    }

    public async Task<Vault?> GetBydIdIncludeShareAndElementsAsync(string id)
    {
        return await _dbContext.Vaults.Include(o => o.Shares).Include(o => o.Elements).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async  Task<(IEnumerable<Element?>, PermissionLevel)> GetSharedElementsByVaultIdAsync(string vaultId)
    {
        var share = await _dbContext.Shares.Include(o => o.Vault)
            .SingleOrDefaultAsync(o => o.UserId == _helper.GetUserId() && o.VaultId == vaultId);

        var listeElement = await _dbContext.Elements.Where(o => o.VaultId == share!.VaultId).ToListAsync();

        return (listeElement, share!.Permission);
    }

    public async Task<IEnumerable<VaultDto?>> GetSharedVaultsAsync()
    {
        var _liste = await _dbContext.Shares.Include(o => o.Vault).Where(o => o.UserId == _helper.GetUserId()).ToListAsync();
        var resultList = new List<VaultDto>();
        if (_liste is not null && _liste.Any())
        {
            foreach (var item in _liste)
            {
                if (item.Vault is not null)
                {
                    resultList.Add(new VaultDto
                    {
                        Id = item.Vault.Id,
                        CreatedAt = item.Vault.CreatedAt,
                        Label = item.Vault.Label,
                        IsPrivate = item.Vault.IsPrivate,
                        Permission = item.Permission,
                        UpdatedAt = item.Vault.UpdatedAt,
                    });
                }
            }
        }
        return resultList;
    }
    public async Task<IEnumerable<Vault?>> GetVaultsAsync()
    {
        return await _dbContext.Vaults.Where(o => o.UserId == _helper.GetUserId()).ToListAsync();
    }
}