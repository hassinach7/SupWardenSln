namespace SupWarden.API.Services.Contracts;

public interface IGroupeAssignmentsService : IBaseService<GroupeAssignment>
{
    Task<IEnumerable<GroupeAssignment>> GetGroupeAssignmentListIncludeGroupAndUserAsync();
    Task<GroupeAssignment?> GetGroupeAssignmentByUserAndGroupIdAsync(string userId, string groupId);
    Task<IEnumerable<GroupeAssignment>> GetListByGroupeIdAsync(string groupId);
}