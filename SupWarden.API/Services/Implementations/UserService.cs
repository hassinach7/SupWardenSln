using Microsoft.EntityFrameworkCore;

namespace SupWarden.API.Services.Implementations;

public class UserService : BaseService<User>, IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(SupWardenDbContext dbContext, UserManager<User> userManager) : base(dbContext)
    {
        _userManager = userManager;
    }
    public async Task<IReadOnlyList<User>> GetUsersIncludeGroupesAsync()
    {
        return await _dbContext.Users.Include(o => o.Groups).ToListAsync();
    }

    public async Task<User?> GetUserIncludeGroupesByIDAsync(string id)
    {
        return await _dbContext.Users.Include(o => o.Groups).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errorMessages = result.Errors
                  .Select(failure => $"Property: {failure.Code}, Error: {failure.Description}");

            throw new ArgumentException($"User is not added. Details: {string.Join(", ", errorMessages)}");
        }
    }

    public async Task DeleteAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errorMessages = result.Errors
                .Select(error => $"Error: {error.Description}");

            throw new ArgumentException($"User could not be deleted. Details: {string.Join(", ", errorMessages)}");
        }
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errorMessages = result.Errors
                  .Select(failure => $"Error: {failure.Description}");

            throw new ArgumentException($"User could not be updated. Details: {string.Join(", ", errorMessages)}");
        }

        return user;
    }

    public async Task<bool> SetPinCodeAsync(int pinCode, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null)
        {
            user.PinCode = pinCode.ToString();
            await _userManager.UpdateAsync(user);

            return true;
        }
        return false;
    }
    public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is not null)
        {
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return true;
            }
        }
        return false;
    }


}