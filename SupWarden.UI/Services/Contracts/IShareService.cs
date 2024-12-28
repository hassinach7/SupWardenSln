using SupWarden.Dto.Dtos.Share;
using SupWarden.UI.ViewModels.Share;

namespace SupWarden.UI.Services.Contracts
{
    public interface IShareService
    {
        Task<bool> InviteMemberToVaultAsync(AddMemberToVaultVM memberVM);
        Task<IEnumerable<ShareDto>?> GetSharedVaultsAsync();
        Task<bool> UpdateShareAsync(UpdateShareVM shareVM);
        Task<bool> DeleteShareAsync(DeleteShareVM shareVM);
    }
}
