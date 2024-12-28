using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupWarden.Dto.Dtos.User;
using SupWarden.API.Helpers;
using SupWarden.Ressource.Ressources.UserRessources.Create;
using SupWarden.Ressource.Ressources.UserRessources.Update;

namespace SupWarden.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly Helper _helper;

    public UserController(IUserService userService, IMapper mapper, Helper helper)
    {
        this._userService = userService;
        this._mapper = mapper;
        this._helper = helper;
    }

    [HttpPost]
    [Route("[action]")]
    [ActionName("Create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserRessource request)
    {
        CreateUserValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (resultValidation.Errors.Any())
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"User is not valid. Details: {string.Join(", ", errorMessages)}");
        }
        var newUser = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber.ToString(),
        };
        await _userService.AddAsync(newUser, request.Password);
        return Ok();
    }
    [Route("[action]")]
    [HttpDelete]
    [ActionName("Delete")]

    public async Task<IActionResult> DeleteAsync(string id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            return Ok(new { message = $"User with ID {id} deleted successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting the user.", error = ex.Message });
        }
    }
    [Route("[action]")]
    [ActionName("Update")]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateUserRessource request)
    {
        UpdateUserValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (!resultValidation.IsValid)
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"User is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber.ToString();

        await _userService.UpdateUserAsync(user);

        return Ok(new { message = "User updated successfully.", user });
    }

    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]

    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _userService.GetAllAsync();
        var returnData = _mapper.Map<List<UserDto>>(users);
        return Ok(returnData);
    }

    [HttpGet]
    [ActionName("GetById")]
    [Route("[action]")]

    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }
        return Ok(_mapper.Map<UserDto>(user));
    }


    [HttpPost]
    [Route("[action]")]
    [ActionName("UpdatePinCode")]
    public async Task<IActionResult> UpdatePinCodeAsync([FromBody] ChangePasswordRessource request)
    {
        var result = await _userService.SetPinCodeAsync(request.PinCode, _helper.GetUserId());
        return Ok(result);
    }


    [HttpPost]
    [Route("[action]")]
    [ActionName("ChangePassword")]
    public async Task<IActionResult> UpdatePasswordAsync([FromBody] ChangePasswordResource request)
    {
        var result = await _userService.ChangePasswordAsync(request.OldPassword, request.NewPassword, _helper.GetUserId());
        return Ok(result);
    }



}

