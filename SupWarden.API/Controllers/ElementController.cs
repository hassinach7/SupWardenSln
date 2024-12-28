using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SupWarden.Dto.Dtos.Element;
using SupWarden.Ressource.Ressources.ElementRessources.Create;
using SupWarden.Ressource.Ressources.ElementRessources.Update;
using SupWarden.Ressource.Validators;

namespace SupWarden.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ElementController : ControllerBase
{
    #region Ctor and Fields
    private readonly IElementService _elementService;
    private readonly IMapper _mapper;
    private readonly IVaultService _vaultService;

    public ElementController(IElementService elementService, IMapper mapper, IVaultService vaultService)
    {
        _elementService = elementService;
        _mapper = mapper;
        _vaultService = vaultService;
    }
    #endregion


    [HttpPost]
    [Route("[action]")]
    [ActionName("Create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateElementRessource request)
    {
        CreateElementValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (resultValidation.Errors.Any())
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"Vault is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        var vault = await _vaultService.GetByIdAsync(request.VaultId);
        if (vault is null)
        {
            return BadRequest(new { message = $"Vault with Id {request.VaultId} cannot be found" });
        }

        var newElement = _mapper.Map<Element>(request);
        var result = await _elementService.AddAsync(newElement);
        return Ok(new { id = result.Id });
    }

    [HttpPut]
    [Route("[action]")]
    [ActionName("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateElementRessource request)
    {
        UpdateElementValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (!resultValidation.IsValid)
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"Request is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        if ((await _elementService.GetByIdAsync(request.Id)) is null)
        {
            return NotFound(new { message = $"The Element with Id {request.Id} is not found" });
        }

        var vault = await _vaultService.GetByIdAsync(request.VaultId);
        if (vault is null)
        {
            return BadRequest(new { message = $"Vault with Id {request.VaultId} cannot be found" });
        }

        var elementToUpdate = _mapper.Map<Element>(request);
        elementToUpdate.UpdatedAt = DateTime.UtcNow;
        await _elementService.UpdateAsync(elementToUpdate);
        return Ok(new { message = "Element updated successfully." });
    }


    [HttpDelete]
    [Route("[action]")]
    [ActionName("Delete")]

    public async Task<IActionResult> DeleteAsync(string id)
    {
        var element = await _elementService.GetByIdAsync(id);
        if (element is null)
        {
            return NotFound(new { message = $"The Element with Id {id} is not found" });
        }
        await _elementService.DeleteAsync(element);
        return Ok(new { message = $"Element with ID {id} deleted successfully." });
    }


    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]

    public async Task<IActionResult> GetAllAsync()
    {
        var elements = await _elementService.GetElementsByCurrentUserAsync();
        var returnData = _mapper.Map<List<ElementDto>>(elements);
        return Ok(returnData);
    }

    [HttpGet]
    [ActionName("GetById")]
    [Route("[action]")]

    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var element = await _elementService.GetByIdWithVaultAsync(id);
        if (element is null)
        {
            return NotFound(new { message = $"User with ID {id} not found." });
        }
        return Ok(_mapper.Map<ElementDto>(element));
    }
}