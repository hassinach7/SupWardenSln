namespace SupWarden.Models.Models;

public class GroupeAssignment
{
    public string UserId { get; set; } = null!;
    public virtual User? User { get; set; }

    public string GroupId { get; set; } = null!;
    public virtual Group? Group { get; set; }
}