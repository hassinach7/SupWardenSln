using SupWarden.API.Helpers;

namespace SupWarden.API.Services.Implementations;

public class GroupeAssignmentsService : BaseService<GroupeAssignment>, IGroupeAssignmentsService
{
    private readonly Helper _helper;

    public GroupeAssignmentsService(SupWardenDbContext dbContext, Helper helper) : base(dbContext)
    {
        _helper = helper;
    }
    public async Task<GroupeAssignment?> GetGroupeAssignmentByUserAndGroupIdAsync(string userId, string groupId)
    {
        return await _dbContext.GroupeAssignments.SingleOrDefaultAsync(o => o.UserId == userId && o.GroupId == groupId);

    }
    public async Task<IEnumerable<GroupeAssignment>> GetGroupeAssignmentListIncludeGroupAndUserAsync()
    {
        return await _dbContext.GroupeAssignments.Include(o => o.User).Include(o => o.Group)
             .Where(o => o.UserId == _helper.GetUserId())
            .ToListAsync();
    }
    public async Task<IEnumerable<GroupeAssignment>> GetListByGroupeIdAsync(string groupId)
    {
        return await _dbContext.GroupeAssignments.Where(o => o.GroupId == groupId).ToListAsync();
    }
}