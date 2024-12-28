using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Auth;

public class LoginVM
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}