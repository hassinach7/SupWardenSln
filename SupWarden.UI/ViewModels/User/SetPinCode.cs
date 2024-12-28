using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.User
{
    public class SetPinCodeVM
    {
        [Required]
        [Display(Name = "New PIN Code")]
        [Range(100000, 999999, ErrorMessage = "The PIN Code must be exactly 6 digits.")]
        public int PinCode { get; set; }


        [Required]
        [Display(Name = "Confirm New PIN Code")]
        [Compare("PinCode", ErrorMessage = "The PIN Code and confirmation do not match.")]
        public int ConfirmPinCode { get; set; }
    }
}
