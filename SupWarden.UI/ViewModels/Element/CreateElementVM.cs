using SupWarden.Dto.Dtos.Vault;
using SupWarden.UI.ViewModels.Vault;

namespace SupWarden.UI.ViewModels.Element;

public class CreateElementVM : CreateElementVaultVM
{
    public IEnumerable<VaultDto>? VaultsList { get; set; } = default!;
}