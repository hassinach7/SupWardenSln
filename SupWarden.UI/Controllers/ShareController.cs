using Microsoft.AspNetCore.Mvc;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.Share;
using System.Threading.Tasks;

namespace SupWarden.UI.Controllers
{
    public class ShareController : Controller
    {
        private readonly IShareService _shareService;

        public ShareController(IShareService shareService)
        {
            _shareService = shareService;
        }

        // GET: Share/SharedVaults
        [HttpGet]
        public async Task<IActionResult> SharedVaults()
        {
            // Obtient l'identifiant de l'utilisateur actuellement connecté
            var userId = User.Identity?.Name; // Assuming user identity is set to userId
            if (userId == null)
            {
                return Unauthorized();
            }

            // Obtient la liste des vaults partagés avec l'utilisateur
            var sharedVaults = await _shareService.GetSharedVaultsAsync();
            return View(sharedVaults); // Assurez-vous que la vue "SharedVaults.cshtml" existe dans /Views/Share/
        }

        // GET: Share/Invite
        [HttpGet]
        public IActionResult InviteMember(string vaultId)
        {
            var model = new AddMemberToVaultVM { VaultId = vaultId };
            return View(model);
        }

        // POST: Share/Invite
        [HttpPost]
       
        public async Task<IActionResult> InviteMember(AddMemberToVaultVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isInvited = await _shareService.InviteMemberToVaultAsync(model);
            if (isInvited)
            {
                ViewData["IsInvited"] = true;
                return RedirectToAction("SharedVaults", "Share");
            }

            ModelState.AddModelError("ApiError", "The member could not be invited. Please try again.");
            return View(model);
        }

        // GET: Share/Edit
        [HttpGet]
        public IActionResult Edit(string userId, string vaultId)
        {
            var model = new UpdateShareVM { UserId = userId, VaultId = vaultId };
            return View(model);
        }

        // POST: Share/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateShareVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isUpdated = await _shareService.UpdateShareAsync(model);
            if (isUpdated)
            {
                ViewData["IsUpdated"] = true;
                return RedirectToAction("SharedVaults", "Share");
            }

            ModelState.AddModelError("ApiError", "The share could not be updated. Please try again.");
            return View(model);
        }

        // GET: Share/Delete
        [HttpGet]
        public IActionResult Delete(string userId, string vaultId)
        {
            var model = new DeleteShareVM { UserId = userId, VaultId = vaultId };
            return View(model);
        }

        // POST: Share/Delete
        [HttpPost]
       
        public async Task<IActionResult> Delete(DeleteShareVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SharedVaults", "Share");
            }

            var isDeleted = await _shareService.DeleteShareAsync(model);
            if (isDeleted)
            {
                return RedirectToAction("SharedVaults", "Share");
            }

            ModelState.AddModelError("ApiError", "The share could not be deleted. Please try again.");
            return RedirectToAction("SharedVaults", "Share");
        }
    }
}
