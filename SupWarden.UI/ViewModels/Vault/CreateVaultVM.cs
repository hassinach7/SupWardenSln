using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Vault
{
    public class CreateVaultVM
    {
        [Required]
        public string Label { get; set; } = null!;

        public bool IsPrivate { get; set; }
    }

}
