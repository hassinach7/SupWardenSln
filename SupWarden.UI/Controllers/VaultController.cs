using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupWarden.Dto.Dtos.Element;
using SupWarden.Dto.Dtos.Vault;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.Vault;
using System.ComponentModel.DataAnnotations;

namespace SupWarden.UI.Controllers
{
    public class VaultController : Controller
    {
        private readonly IVaultService _vaultService;
        private readonly IMapper _mapper;
        private readonly IGroupeService _groupeService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VaultController(IVaultService vaultService, IMapper mapper , IGroupeService groupeService,
                              IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            this._vaultService = vaultService;
            this._mapper = mapper;
            this._groupeService = groupeService;
            this._userService = userService;
            this._httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Share(string id)
        {
            var vault = await _vaultService.GetVaultByIdAsync(id);
            if(vault is null)
            {
                return NotFound();
            }
            var model = new ShareVaultVM
            {
                VaultId = id,
                VaultName = vault.Label
            };
            model = await Set(model);
            return View(model);
        }

        private async Task<ShareVaultVM> Set(ShareVaultVM model)
        {
            var groupes = await _groupeService.GetAllGroupesAsync();
            var users = await _userService.GetAllUsersAsync();
            var currentUserEmail = _httpContextAccessor!.HttpContext!.Session.GetString("Email");

            model.Groups = groupes;
            model.Users = users!.Where(o => o.Email != currentUserEmail).ToList();
            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Share(ShareVaultVM model)
        {
            if(model.GroupId is null && model.UserId is null)
            {
                ModelState.AddModelError("", "Please Select 'User' or 'Groupe' Or 'Both'");
                model = await Set(model);
            }
            else
            {
                var result = await _vaultService.ShareAsync(model);
                if (result)
                {
                    model = await Set(model);
                    ViewData["IsShared"] = true;
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var vaults = await _vaultService.GetVaultsAsync();
            return View(vaults);
        }

        // GET: Vault/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateVaultVM();
            return View(model);
        }

        // POST: Vault/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVaultVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _vaultService.CreateVaultAsync(model);
            if (result is not null)
            {
                ViewData["IsCreated"] = true;
                return RedirectToAction("List", "Vault");
            }

            return View(model);
        }


        // GET: Vault/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(List));
            }

            // Récupérer le vault à modifier
            VaultDto? vaultToUpdate = await _vaultService.GetVaultByIdAsync(id);
            if (vaultToUpdate is null)
            {
                return Redirect(nameof(List));
            }

            var model = _mapper.Map<UpdateVaultVM>(vaultToUpdate);
            return View(model);
        }

        // POST: Vault/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateVaultVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _vaultService.UpdateVaultAsync(model);
            return RedirectToAction("List", "Vault");
        }

  
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(List));
            }

            VaultDto? vaultToDelete = await _vaultService.GetVaultByIdAsync(id);
            if (vaultToDelete is null)
            {
                return NotFound();
            }

            var model = _mapper.Map<DeleteVaultVM>(vaultToDelete);

            return View(model);
        }

        // POST: Vault/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteVaultVM model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(nameof(List));

            }

            await _vaultService.DeleteVaultAsync(model.Id);
            return RedirectToAction("List", "Vault");
        }

        [HttpGet]
        public async Task<IActionResult> SharedList()
        {
            var vaults = await _vaultService.GetSharedVaultsAsync();
            return View(vaults);
        }

        [HttpGet]
        public IActionResult CreateElement(string? vaultId)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(nameof(List));
            }
            var model = new CreateElementVaultVM() { VaultId = vaultId! };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SharedElement([Required]string? id)
        {
            if(!ModelState.IsValid)
            {
                return Redirect(nameof(SharedList));
            }
            var sharedElements = await _vaultService.GetSharedEelementsByVaultIdAsync(id!);
            return View(sharedElements);
        }
    }
}
