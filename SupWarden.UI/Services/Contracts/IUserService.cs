using SupWarden.Dto.Dtos.User;
using SupWarden.UI.ViewModels.User;

namespace SupWarden.UI.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>?> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(string id);  

        Task<bool> UpdateUserAsync(string userId, UserDto userDto);
        Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<bool> SetPinCodeAsync(int pinCode);
        Task<bool> VerifyPinCodeAsync(string userId, string pinCode);
        Task<bool> AddUserAsync(CreateUserVM createUserVM);
    }
}
