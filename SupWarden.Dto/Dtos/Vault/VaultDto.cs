using SupWarden.Models.Enums;

namespace SupWarden.Dto.Dtos.Vault;

public class VaultDto
{
    public string Id { get; set; } = null!;
    public string Label { get; set; } = null!;
    public bool IsPrivate { get; set; }
    public PermissionLevel  Permission  { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}