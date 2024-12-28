using SupWarden.Dto.GroupDtos;
using SupWarden.UI.ViewModels.Group;
using SupWarden.UI.ViewModels.Vault;

namespace SupWarden.UI.Services.Contracts;

public interface IGroupeService
{
    Task<IEnumerable<GroupDto>?> GetAllGroupesAsync();
    Task<GroupDto?> GetGroupByIdAsync(string id);
    Task<ReturnCreatedGroupeVM?> CreateGroupeAsync(CreateGroupeVM groupeVM);
    Task<bool> DeleteGroupeAsync(string groupId);
    Task<bool> UpdateGroupeAsync(UpdateGroupeVM groupeVM);
    Task<bool> AssignAsync(AssignGroupeVM request);
}
