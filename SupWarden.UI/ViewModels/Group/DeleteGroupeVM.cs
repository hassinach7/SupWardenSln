using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Group
{
    public class DeleteGroupeVM
    {
        [Required]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
