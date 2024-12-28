using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Group
{
    public class CreateGroupeVM
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
