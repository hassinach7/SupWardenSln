using AutoMapper;
using SupWarden.Dto.GroupDtos;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.Group;

namespace SupWarden.UI.Controllers;

public class GroupeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGroupeService _groupeService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GroupeController(ILogger<HomeController> logger, IGroupeService groupeService, IMapper mapper, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
       this._logger = logger;
       this._groupeService = groupeService;
       this._mapper = mapper;
       this._userService = userService;
       this._httpContextAccessor = httpContextAccessor;
        
    }

    [HttpGet]
    public async Task<IActionResult> Assign(string id)
    {
        var group = await _groupeService.GetGroupByIdAsync(id);
        if (group == null)
        {
            return NotFound();
        }

        var model = new AssignGroupeVM
        {
            GroupId = id,
            GroupName = group.Name
        };
        model = await SetAssignModel(model);
        return View(model);
    }

    private async Task<AssignGroupeVM> SetAssignModel(AssignGroupeVM model)
    {
        var users = await _userService.GetAllUsersAsync();
        var currentUserEmail = _httpContextAccessor.HttpContext!.Session.GetString("Email");

        model.Users = users!.Where(user => user.Email != currentUserEmail).ToList();
        return model;
    }

    [HttpPost]
    public async Task<IActionResult> Assign(AssignGroupeVM model)
    {
        if (string.IsNullOrEmpty(model.UserId))
        {
            ModelState.AddModelError("", "Please select a user.");
            model = await SetAssignModel(model);
        }
        else
        {
            var result = await _groupeService.AssignAsync(model);
            if (result)
            {
                model = await SetAssignModel(model);
                ViewData["IsAssigned"] = true;
                TempData["SuccessMessage"] = "User successfully assigned to the group.";
                return RedirectToAction("List", "Groupe");
            }
        }

        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> List()
    {
        var data = await _groupeService.GetAllGroupesAsync();
        return View(data);
    }

    // GET: Group/Create
    [HttpGet]
    public IActionResult Create()
    {
        var model = new CreateGroupeVM();
        return View(model);
    }

    // POST: Group/Create
    [HttpPost]
  
    public async Task<IActionResult> Create(CreateGroupeVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _groupeService.CreateGroupeAsync(model);
        if (result is not null)
        {
            ViewData["IsCreated"] = true;
            return RedirectToAction("List", "Groupe");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(List));
        }

        GroupDto? groupToDelete = await _groupeService.GetGroupByIdAsync(id);
        if (groupToDelete is null)
        {
            return NotFound();
        }

        var model = _mapper.Map<DeleteGroupeVM>(groupToDelete);

        return View(model);
    }

    // POST: Group/Delete
    [HttpPost]
    public async Task<IActionResult> Delete(DeleteGroupeVM model)
    {
        if (!ModelState.IsValid)
        {
            return Redirect(nameof(List));

        }

        await _groupeService.DeleteGroupeAsync(model.Id);
        return RedirectToAction("List", "Groupe");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(List));
        }

        // Récupérer le groupe à modifier
        GroupDto? groupToUpdate = await _groupeService.GetGroupByIdAsync(id);
        if (groupToUpdate is null)
        {
            return Redirect(nameof(List));
        }

        var model = _mapper.Map<UpdateGroupeVM>(groupToUpdate);
        return View(model);
    }

    // POST: Vault/Edit
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateGroupeVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _groupeService.UpdateGroupeAsync(model);
        return RedirectToAction("List", "Groupe");
    }

}



