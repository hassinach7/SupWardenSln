namespace SupWarden.Dto.Dtos.GroupeAssignment;

public class GroupeAssignmentDto
{
    public GroupeAssignmentUserDto User { get; set; } = null!;
    public GroupeAssignmentGroupDto  Group { get; set; } = null!;
}

public class GroupeAssignmentUserDto
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class GroupeAssignmentGroupDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}