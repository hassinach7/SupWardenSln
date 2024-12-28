using SupWarden.Models.Enums;

namespace SupWarden.Ressource.Ressources.ShareRessources.Update;

public class UpdateShareResource
{
    public PermissionLevel Permission { get; set; }
    public string VaultId { get; set; } = null!;
    public string UserId { get; set; } = null!;
}