using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Group
{
    public class UpdateGroupeVM
    {

      
            [Required]
            public string Id { get; set; } = null!;

            [Required(ErrorMessage = "The group Name is required.")]
            public string Name { get; set; } = null!;

    }

}




