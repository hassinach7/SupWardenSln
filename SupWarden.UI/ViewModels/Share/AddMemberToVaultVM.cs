using SupWarden.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Share
{
    public class AddMemberToVaultVM
    {
        [Required]
        public string VaultId { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public PermissionLevel Permission { get; set; }
    }
}


