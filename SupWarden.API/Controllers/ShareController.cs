using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupWarden.Dto.Dtos;
using SupWarden.Dto.Dtos.Share;
using SupWarden.API.Helpers;
using SupWarden.Ressource.Ressources.ShareRessources.Create;
using SupWarden.Ressource.Ressources.ShareRessources.Delete;
using SupWarden.Ressource.Ressources.ShareRessources.Update;
using SupWarden.API.Validators;


namespace SupWarden.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ShareController : ControllerBase
{
    #region Constructor and Fields
    private readonly IShareService _shareService;
    private readonly IMapper _mapper;
    private readonly Helper _helper;
    private readonly IVaultService _vaultService;
    private readonly IUserService _userService;

    public ShareController(IShareService shareService, IMapper mapper, Helper helper, IVaultService vaultService,
        IUserService userService)
    {
        _shareService = shareService;
        _mapper = mapper;
        _helper = helper;
        _vaultService = vaultService;
        _userService = userService;
    }

    #endregion
    
    [HttpGet]
    [Route("[action]")]
    [ActionName("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var shares = await _shareService.GetShareListIncludeVaultAndUserAsync(_helper.GetUserId());
        var mappedData = _mapper.Map<IEnumerable<ShareDto>>(shares);
        return Ok(mappedData);
    }

    // POST: api/share/addmember
    [HttpPost]
    [Route("[action]")]
    [ActionName("Create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateShareResource request)
    {

        CreateShareValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (resultValidation.Errors.Any())
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"Share Request is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        var vault = await _vaultService.GetByIdAsync(request.VaultId);
        if (vault is null)
        {
            return BadRequest(new { message = $"Vault with Id {request.VaultId} cannot be found" });
        }

        var user = await _userService.GetByIdAsync(request.UserId);
        if (user is null)
        {
            return BadRequest(new { message = $"User with Id {request.UserId} cannot be found" });
        }

        var existingShare = await _shareService.GetShareByUserAndVaultIdAsync(request.UserId, request.VaultId);
        if (existingShare is not null)
        {
            return BadRequest(new { message = $"This Share already exist for the user with Id {request.UserId} and Vault id {request.VaultId}" });
        }

        await _shareService.AddAsync(new Share()
        {
            UserId = user.Id,
            VaultId = request.VaultId,
            Permission = request.Permission
        });

        return Ok();
    }

    [HttpPut]
    [Route("[action]")]
    [ActionName("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateShareResource request)
    {
        // Validate the request
        UpdateShareValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (!resultValidation.IsValid)
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");
            return BadRequest(new { message = $"Request is not valid. Details: {string.Join(", ", errorMessages)}" });
        }
        var shareToUpdated = await _shareService.GetShareByUserAndVaultIdAsync(request.UserId, request.VaultId);
        if (shareToUpdated is null)
        {
            return BadRequest(new { message = $"This Share is not exist for the user with Id {request.UserId} and Vault id {request.VaultId}" });
        }
        shareToUpdated.Permission = request.Permission;
        await _shareService.UpdateAsync(shareToUpdated);
        return NoContent();
    }

    // DELETE: api/share/delete?vaultId={vaultId}&memberId={memberId}
    [HttpDelete]
    [Route("[action]")]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteShareResource request)
    {
        // Validate the request
        DeleteShareValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (!resultValidation.IsValid)
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");
            return BadRequest(new { message = $"Request is not valid. Details: {string.Join(", ", errorMessages)}" });
        }
        var shareToDelete = await _shareService.GetShareByUserAndVaultIdAsync(request.UserId, request.VaultId);
        if (shareToDelete is null)
        {
            return BadRequest(new { message = $"This Share is not exist for the user with Id {request.UserId} and Vault id {request.VaultId}" });
        }
        await _shareService.DeleteAsync(shareToDelete);
        return NoContent();
    }
}