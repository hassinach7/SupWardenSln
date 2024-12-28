using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SupWarden.Dto.Dtos.GroupeAssignment;
using SupWarden.Ressource.Ressources.GroupeAssignmentRessources.Create;
using SupWarden.Ressource.Ressources.GroupeAssignmentRessources.Delete;

namespace SupWarden.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupeAssignmentsController : ControllerBase
{
    #region Constructor and Fields
    private readonly IGroupeAssignmentsService _groupeAssignmentsService;
    private readonly IMapper _mapper;
    private readonly IGroupService _groupService;
    private readonly IUserService _userService;

    public GroupeAssignmentsController(IGroupeAssignmentsService groupeAssignmentsService,
                                       IMapper mapper, IGroupService groupService,
                                       IUserService userService)
    {
        _groupeAssignmentsService = groupeAssignmentsService;
        _mapper = mapper;
        _groupService = groupService;
        _userService = userService;
    }

    #endregion

    [HttpGet]
    [Route("[action]")]
    [ActionName("GetAll")]
    public async Task<IActionResult> GetAllAsync()
    {
        var groupeAssignmentsList = await _groupeAssignmentsService.GetGroupeAssignmentListIncludeGroupAndUserAsync();
        var mappedData = _mapper.Map<IEnumerable<GroupeAssignmentDto>>(groupeAssignmentsList);
        return Ok(mappedData);
    }

    // POST: api/share/addmember
    [HttpPost]
    [Route("[action]")]
    [ActionName("Create")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGroupeAssignmentRessource request)
    {

        CreateGroupeAssignmentValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (resultValidation.Errors.Any())
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");

            throw new ArgumentException($"GroupeAssignment Request is not valid. Details: {string.Join(", ", errorMessages)}");
        }

        var group = await _groupService.GetByIdAsync(request.GroupId);
        if (group is null)
        {
            return BadRequest(new { message = $"Group with Id {request.GroupId} cannot be found" });
        }

        var user = await _userService.GetByIdAsync(request.UserId);
        if (user is null)
        {
            return BadRequest(new { message = $"User with Id {request.UserId} cannot be found" });
        }

        var existingGS = await _groupeAssignmentsService.GetGroupeAssignmentByUserAndGroupIdAsync(request.UserId, request.GroupId);
        if (existingGS is not null)
        {
            return BadRequest(new { message = $"This Group Assignements already exist for the user with Id {request.UserId} and Group id {request.GroupId}" });
        }

        await _groupeAssignmentsService.AddAsync(new GroupeAssignment()
        {
            UserId = user.Id,
            GroupId = request.GroupId
        });

        return Ok();
    }

    [HttpDelete]
    [Route("[action]")]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteAsync([FromBody] DeleteGroupeAssignmentRessource request)
    {
        // Validate the request
        DeleteGroupeAssignmentValidation validator = new();
        var resultValidation = await validator.ValidateAsync(request);

        if (!resultValidation.IsValid)
        {
            var errorMessages = resultValidation.Errors
                .Select(failure => $"Property: {failure.PropertyName}, Error: {failure.ErrorMessage}");
            return BadRequest(new { message = $"Request is not valid. Details: {string.Join(", ", errorMessages)}" });
        }
        var gsToDelete = await _groupeAssignmentsService.GetGroupeAssignmentByUserAndGroupIdAsync(request.UserId, request.GroupId);
        if (gsToDelete is null)
        {
            return BadRequest(new { message = $"This GroupAssignement is not exist for the user with Id {request.UserId} and GroupId id {request.GroupId}" });
        }
        await _groupeAssignmentsService.DeleteAsync(gsToDelete);
        return NoContent();
    }
}
