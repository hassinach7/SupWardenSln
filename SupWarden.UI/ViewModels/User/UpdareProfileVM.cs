using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.User
{
   
        public class UpdateProfileVM
        {
             [Required]
             public string Id { get; set; } = null!;

            [Required]
            [Display(Name = "First Name")]
            [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
            public string FirstName { get; set; } = null!;

            [Required]
            [Display(Name = "Last Name")]
            [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
            public string LastName { get; set; } = null!;

            [Required]
            [EmailAddress]
            [Display(Name = "Email Address")]
            public string Email { get; set; } = null!;

            [Phone]
            [Display(Name = "Phone Number")]
            public string? PhoneNumber { get; set; }
        }
    }




