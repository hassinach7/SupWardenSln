using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupWarden.API.Helpers;
using SupWarden.Dto.Dtos.Element;
using SupWarden.Dto.Dtos.Vault;
using SupWarden.Ressource.Ressources.VaultRessources.Create;
using SupWarden.Ressource.Ressources.VaultRessources.Share;
using SupWarden.Ressource.Ressources.VaultRessources.Update;
using System.ComponentModel.DataAnnotations;

namespace SupWarden.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VaultController : ControllerBase
{
    private readonly IVaultService _vaultService;
    private readonly Helper _helper;
    private readonly IMapper _mapper;
    private readonly IShareService _shareService;
    private readonly IGroupeAssignmentsService _groupeAssignmentsService;

    public VaultController(IVaultService vaultService, Helper helper, IMapper mapper, IShareService shareService,
        IGroupeAssignmentsService groupeAssignmentsService)
    {
        this._vaultService = vaultService;
        this._helper = helper;
        this._mapper = mapper;
        this._shareService = shareService;
        this._groupeAssignmentsService = groupeAssignmentsService;
    }

    [HttpPost]
    [Route("[action]")]
    [ActionName("Create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateVaultRessource request)
    {
        CreateVaultValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (resultValidation.Errors.Any())
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"Vault is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        var newVault = new Vault()
        {
            Label = request.Label,
            UserId = _helper.GetUserId()
        };

        var result = await _vaultService.AddAsync(newVault);

        return Ok(new { id = result.Id });
    }


    [HttpPut]
    [Route("[action]")]
    [ActionName("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateVaultRessource request)
    {
        UpdateVaultValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (!resultValidation.IsValid)
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"Request is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        var vault = await _vaultService.GetByIdAsync(request.Id);
        if (vault is null)
        {
            return NotFound(new { message = $"The Vault with Id {request.Id} is not found" });
        }
        if (vault!.UserId != _helper.GetUserId())
        {
            return NotFound(new { message = $"The Vault with Id {request.Id} is not found" });
        }

        vault.Label = request.Label;
        vault.UpdatedAt = DateTime.UtcNow;
        vault.IsPrivate = request.IsPrivate;

        await _vaultService.UpdateAsync(vault);

        return NoContent();
    }

    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]

    public async Task<IActionResult> GetAllAsync()
    {
        var vaultList = await _vaultService.GetVaultsAsync();
        var mappedData = _mapper.Map<IEnumerable<VaultDto>>(vaultList);
        return Ok(mappedData);
    }

    [HttpGet]
    [ActionName("GetById")]
    [Route("[action]")]

    public async Task<IActionResult> GetByIdAsync([Required] string id)
    {
        var vault = await _vaultService.GetByIdAsync(id);
        if (vault is null)
            return NotFound(new { message = $"The Vault with Id {id} is not found" });

        var mappedData = _mapper.Map<VaultDto>(vault);
        return Ok(mappedData);
    }

    [HttpDelete]
    [Route("[action]")]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteAsync([Required] string id)
    {
        var vault = await _vaultService.GetBydIdIncludeShareAndElementsAsync(id);
        if (vault is null)
        {
            return NotFound(new { message = $"The Vault with Id {id} is not found" });
        }
        if (vault!.UserId != _helper.GetUserId())
        {
            return NotFound(new { message = $"The Vault with Id {id} is not found" });
        }
        if (vault!.Elements!.Any())
        {
            return NotFound(new { message = $"The Vault  ID {id} cannot deleted because it contains elements" });
        }

        if (vault!.Shares!.Any())
        {
            return NotFound(new { message = $"The Vault  ID {id} cannot deleted because it contains shares" });
        }
        await _vaultService.DeleteAsync(vault);

        return NoContent();
    }

    [HttpPost]
    [Route("[action]")]
    [ActionName("Share")]
    public async Task<IActionResult> ShareAsync([FromBody] ShareVaultRessource request)
    {
        if (request.UserId is not null)
        {
            await SetUserShare(request.UserId, request.VaultId, request.PermissionLevel!.Value);
        }
        if (request.GroupId is not null)
        {
            var gaList = await _groupeAssignmentsService.GetListByGroupeIdAsync(request.GroupId);
            foreach (var ga in gaList)
            {
                await SetUserShare(ga.UserId, request.VaultId, request.PermissionLevel!.Value);
            }
        }
        return Ok();
    }

    private async Task SetUserShare(string userId, string vaultId, PermissionLevel permission)
    {
        var share = await _shareService.GetShareByUserAndVaultIdAsync(userId, vaultId);
        if (share is null)
        {
            // create new Share
            await _shareService.AddAsync(new Share
            {
                UserId = userId,
                VaultId = vaultId,
                Permission = permission
            });
        }
        else
        {
            // update Premission
            share.Permission = permission;
            await _shareService.UpdateAsync(share);
        }
    }

    [HttpGet]
    [Route("[action]")]
    [ActionName("SharedVaults")]
    public async Task<IActionResult> SharedVaultsAsync()
    {
        var vaultList = await _vaultService.GetSharedVaultsAsync();
        var mappedData = _mapper.Map<IEnumerable<VaultDto>>(vaultList);
        return Ok(mappedData);
    }

    [HttpGet]
    [Route("[action]")]
    [ActionName("SharedElements")]
    public async Task<IActionResult> SharedElementsAsync(string id)
    {
        var result = await _vaultService.GetSharedElementsByVaultIdAsync(id);
        var mappedData = _mapper.Map<IEnumerable<ElementDto>>(result.Item1);
        mappedData = mappedData.Select(o =>
        {
            o.Permission = result.Item2;
            return o;
        }).ToList();
        return Ok(mappedData);
    }
}
