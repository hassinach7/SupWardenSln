
using Microsoft.AspNetCore.Identity;

namespace SupWarden.Models.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PinCode { get; set; }
    public virtual IEnumerable<Group>? Groups { get; set; }
    public virtual IEnumerable<GroupeAssignment>? GroupeAssignments { get; set; }
    public virtual IEnumerable<Share>? SharedVaults { get; set; }
    public virtual IEnumerable<Vault>? CreatedVaults { get; set; }
}