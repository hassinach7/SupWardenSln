
namespace SupWarden.Dto.GroupDtos;

public class GroupDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IEnumerable<GroupeGroupeAssignmentDto>? GroupeAssignments { get; set; } 
}

public class GroupeUserDto
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
public class GroupeGroupeAssignmentDto
{
    public GroupeUserDto User { get; set; } = null!;
}