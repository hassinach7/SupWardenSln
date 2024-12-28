using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Vault
{
    public class UpdateVaultVM
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "The vault label is required.")]
        public string Label { get; set; } = null!;

        public bool IsPrivate { get; set; }
    }
}
