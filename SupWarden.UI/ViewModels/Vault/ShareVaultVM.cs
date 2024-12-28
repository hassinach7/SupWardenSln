using SupWarden.Dto.Dtos.User;
using SupWarden.Dto.GroupDtos;
using SupWarden.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.ViewModels.Vault;

public class ShareVaultVM
{
    [Required]
    public string VaultId { get; set; } = null!;
    public string VaultName { get; set; } = null!;
    public IEnumerable<UserDto>?  Users { get; set; }
    [Display(Name = "Select The User")]
    public string? UserId { get; set; }
    public IEnumerable<GroupDto>?  Groups { get; set; }
    [Display(Name = "Select The Group")]
    public string? GroupId { get; set; }
    [Required]
    [Display(Name = "Select The Permission")]
    public PermissionLevel?  PermissionLevel { get; set; }
}