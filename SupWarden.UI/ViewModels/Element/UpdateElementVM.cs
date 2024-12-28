using SupWarden.Dto.Dtos.Vault;
using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Element
{
    public class UpdateElementVM
    {
        [Required]
        public string Id { get; set; } = null!;
        public IEnumerable<VaultDto>? VaultsList { get; set; } = default!;

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

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? TypePassword { get; set; }
    }
}