using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Vault
{
    public class UpdateElementVaultVM
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        public string? Identifiant { get; set; }

        public string? IdentifiantSup { get; set; }

        public string? Password { get; set; }

        public string? Uri { get; set; }

        public string? Note { get; set; }

        public string? NoteSup { get; set; }

        public string? TypePassword { get; set; }

        public string? PieceJointe { get; set; }

        public bool IsSensible { get; set; }

        [Required]
        public string VaultId { get; set; } = null!;

    }
}
