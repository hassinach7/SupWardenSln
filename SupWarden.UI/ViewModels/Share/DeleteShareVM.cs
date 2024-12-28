namespace SupWarden.UI.ViewModels.Share
{
    public class DeleteShareVM
    {
        // ID de l'utilisateur que l'on souhaite retirer du partage
        public string UserId { get; set; } = null!;

        // ID du vault partagé auquel on souhaite retirer l'accès à cet utilisateur
        public string VaultId { get; set; } = null!;
    }
}
