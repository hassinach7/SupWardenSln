
namespace SupWarden.Models.Models;

public class Vault : BaseEntity
{
    public string Label { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public virtual User? Creator { get; set; }
    public bool IsPrivate { get; set; }
    public virtual IEnumerable<Element>? Elements { get; set; }
    public virtual IEnumerable<Share>? Shares { get; set; }
}