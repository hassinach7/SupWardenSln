using SupWarden.Models.Enums;

namespace SupWarden.Dto.Dtos.Share;

public class ShareDto
{
    public ShareUserDto User { get; set; } = null!;
    public ShareVaultDto Vault { get; set; } = null!;
    public PermissionLevel Permission { get; set; }
}

public class ShareUserDto
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class ShareVaultDto
{
    public string Id { get; set; } = null!;
    public string Label { get; set; } = null!;
}
