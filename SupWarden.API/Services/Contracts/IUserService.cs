namespace SupWarden.API.Services.Contracts;

public interface IUserService : IBaseService<User>
{
    Task<IReadOnlyList<User>> GetUsersIncludeGroupesAsync();
    Task<User?> GetUserIncludeGroupesByIDAsync(string id);
    Task AddAsync(User user, string password);
    Task DeleteAsync(string userId);
    Task<User> UpdateUserAsync(User user);
    Task<bool> SetPinCodeAsync(int pinCode , string userId);
    Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
}