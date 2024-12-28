namespace SupWarden.Models.Models;

public class Group : BaseEntity
{
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public virtual User? User { get; set; }
    public virtual IEnumerable<GroupeAssignment>? GroupeAssignments { get; set; }
}