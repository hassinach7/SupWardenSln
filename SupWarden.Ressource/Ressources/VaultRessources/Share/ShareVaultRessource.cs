using SupWarden.Models.Enums;

namespace SupWarden.Ressource.Ressources.VaultRessources.Share;

public class ShareVaultRessource
{
    public string VaultId { get; set; } = null!;
    public string? UserId { get; set; }
    public string? GroupId { get; set; }
    public PermissionLevel? PermissionLevel { get; set; }
}