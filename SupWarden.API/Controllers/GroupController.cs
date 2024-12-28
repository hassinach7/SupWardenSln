using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupWarden.Dto.GroupDtos;
using SupWarden.API.Helpers;
using SupWarden.Ressource.Ressources.GroupRessources.Create;
using SupWarden.Ressource.Ressources.GroupRessources.Update;
using System.ComponentModel.DataAnnotations;
using SupWarden.Ressource.Ressources.GroupRessources.Assign;

namespace SupWarden.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly Helper _helper;
    private readonly IMapper _mapper;
    private readonly IGroupeAssignmentsService _groupeAssignmentsService;

    public GroupController(IGroupService groupService, Helper helper, IMapper mapper, IGroupeAssignmentsService _groupeAssignmentsService)
    {
        this._groupService = groupService;
        this._helper = helper;
        this._mapper = mapper;
        this._groupeAssignmentsService = _groupeAssignmentsService;
    }

    [HttpPost]
    [Route("[action]")]
    [ActionName("Create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGroupRessource request)
    {
        CreateGroupValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (resultValidation.Errors.Any())
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"Group is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        var newGroup = new Group()
        {
            Name = request.Name,
            UserId = _helper.GetUserId()
        };

        var result = await _groupService.AddAsync(newGroup);

        return Ok(new { id = result.Id });
    }

    [HttpPut]
    [Route("[action]")]
    [ActionName("Update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateGroupRessource request)
    {
        UpdateGroupValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (resultValidation.Errors.Any())
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"Request is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        var group = await _groupService.GetByIdAsync(request.Id);
        if (group is null)
        {
            return NotFound(new { message = $"The Group with Id {request.Id} is not found" });
        }

        group.Name = request.Name;
        group.UpdatedAt = DateTime.UtcNow;

        await _groupService.UpdateAsync(group);

        return NoContent();
    }

    [HttpGet]
    [ActionName("GetAll")]
    [Route("[action]")]
    public async Task<IActionResult> GetAllAsync()
    {
        var groupList = await _groupService.GetAllIncludeAssignementsAsync();
        var mappedData = _mapper.Map<IEnumerable<GroupDto>>(groupList);
        return Ok(mappedData);
    }

    [HttpGet]
    [ActionName("GetById")]
    [Route("[action]")]
    public async Task<IActionResult> GetByIdAsync([Required] string id)
    {
        var group = await _groupService.GetByIdIncludeAssignementsAsync(id);
        if (group is null)
            return NotFound(new { message = $"The Group with Id {id} is not found" });

        var mappedData = _mapper.Map<GroupDto>(group);
        return Ok(mappedData);
    }

    [HttpDelete]
    [Route("[action]")]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteAsync([Required] string id)
    {
        await _groupService.DeleteWithIncludeAsync(id);
        return NoContent();
    }

    [HttpPost]
    [Route("[action]")]
    [ActionName("Assign")]
    public async Task<IActionResult> AssignAsync([FromBody] AssignGroupeRessource request)
    {
        if (request.UserId is not null)
        {
            // Assign an individual user
            await AssignUserToGroup(request.UserId, request.GroupId!);
        }

        if (request.GroupId is not null)
        {
            // Assign all users in the group
            var gaList = await _groupeAssignmentsService.GetListByGroupeIdAsync(request.GroupId);
            foreach (var ga in gaList)
            {
                await AssignUserToGroup(ga.UserId, request.GroupId);
            }
        }

        return Ok();
    }

    private async Task AssignUserToGroup(string userId, string groupId)
    {
        // Check if the user is already assigned to the group
        var assignmentExists = await _groupeAssignmentsService.GetGroupeAssignmentByUserAndGroupIdAsync(userId, groupId);
        if (assignmentExists == null)
        {
            // Assign the user to the group
            var groupeAssignment = new GroupeAssignment
            {
                UserId = userId,
                GroupId = groupId
            };

            await _groupeAssignmentsService.AddAsync(groupeAssignment);
        }
    }


}
