using SupWarden.Dto.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Group
{
    public class AssignGroupeVM
    {
        [Required]
        public string GroupId { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public IEnumerable<UserDto>? Users { get; set; }
        [Display(Name = "Select The User")]
        public string? UserId { get; set; }

    }
}
