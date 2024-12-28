
using SupWarden.Models.Enums;

namespace SupWarden.Ressource.Ressources.ShareRessources.Create
{
    public class CreateShareResource
    {
        public string UserId { get; set; } = null!;
        public string VaultId { get; set; } = null!;
        public PermissionLevel Permission { get; set; }
    }
}
