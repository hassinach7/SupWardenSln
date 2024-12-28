namespace SupWarden.API.Services.Contracts;

public interface IGroupService : IBaseService<Group>
{
    Task<IEnumerable<Group>> GetAllIncludeAssignementsAsync();
    Task<Group?> GetByIdIncludeAssignementsAsync(string id);
    Task DeleteWithIncludeAsync(string id);
}