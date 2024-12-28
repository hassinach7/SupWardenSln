using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Vault
{
    public class CreateElementVaultVM
    {
        [Required(ErrorMessage = "The Vault Is Required")]
        public string VaultId { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Identifiant { get; set; } = null!;

        public string? IdentifiantSup { get; set; }

        public string? Uri { get; set; }

        public string? Note { get; set; }

        public string? NoteSup { get; set; }

        public IFormFile? PieceJointe { get; set; }

        public bool IsSensible { get; set; } = true;
    }
}
