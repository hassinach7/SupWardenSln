using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Vault
{
    public class DeleteVaultVM
    {

        [Required]
        public string Id { get; set; } = null!;

        public string Label { get; set; } = null!;

        public bool IsPrivate { get; set; } = true;
    }
}
