using SupWarden.API.Helpers;

namespace SupWarden.API.Services.Implementations;

public class GroupService : BaseService<Group>, IGroupService
{
    private readonly Helper _helper;

    public GroupService(SupWardenDbContext dbContext, Helper helper) : base(dbContext)
    {
        _helper = helper;
    }

    public async Task DeleteWithIncludeAsync(string id)
    {
        using (var db = await _dbContext.Database.BeginTransactionAsync())
        {
            var groupe = await _dbContext.Groups.FindAsync(id);
            if (groupe is null)
            {
                throw new NullReferenceException($"Group with is {id} is not exit");
            }
            var listeAssignements = await _dbContext.GroupeAssignments.Where(o => o.GroupId == id).ToListAsync();
            if (listeAssignements.Any())
            {
                _dbContext.GroupeAssignments.RemoveRange(listeAssignements);
            }
            _dbContext.Groups.Remove(groupe);

            await _dbContext.SaveChangesAsync();
            await db.CommitAsync();
        }
    }

    public async Task<IEnumerable<Group>> GetAllIncludeAssignementsAsync()
    {
        return await _dbContext.Groups.Include(o => o.GroupeAssignments!).ThenInclude(o => o.User)
            .Where(o => o.UserId == _helper.GetUserId()).ToListAsync();
    }

    public async Task<Group?> GetByIdIncludeAssignementsAsync(string id)
    {
        return await _dbContext.Groups.Include(o => o.GroupeAssignments).SingleOrDefaultAsync(o => o.Id == id);
    }
}