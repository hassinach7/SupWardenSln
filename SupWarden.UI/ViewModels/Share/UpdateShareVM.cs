using SupWarden.Models.Enums;

namespace SupWarden.UI.ViewModels.Share
{
    public class UpdateShareVM
    {
        // ID de l'utilisateur pour lequel les permissions doivent être mises à jour
        public string UserId { get; set; } = null!;

        // ID du vault partagé
        public string VaultId { get; set; } = null!;

        // Nouveau niveau de permission (Creator, Editor, Reader)
        public PermissionLevel Permission { get; set; }
    }
}
