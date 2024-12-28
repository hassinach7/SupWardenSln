using AutoMapper;
using SupWarden.Dto.Dtos.Element;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.Element;

namespace SupWarden.UI.Controllers
{
    public class ElementController : Controller
    {
        private readonly IElementService _elementService;
        private readonly IVaultService _vaultService;
        private readonly IMapper _mapper;

        public ElementController(IElementService elementService, IVaultService vaultService, IMapper mapper)
        {
            this._elementService = elementService;
            this._vaultService = vaultService;
            this._mapper = mapper;
        }

        // GET: Element/List
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var elements = await _elementService.GetElementsAsync();
            return View(elements);
        }

        // GET: Element/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vaultList = await _vaultService.GetVaultsAsync();
            var model = new CreateElementVM() { VaultsList = vaultList };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateElementVM model)
        {
            if (!ModelState.IsValid)
            {
                model.VaultsList = await _vaultService.GetVaultsAsync();
                return View(model);
            }

            var result = await _elementService.CreateElementAsync(model);
            if (result is not null)
            {
                ViewData["IsCreated"] = true;
                return RedirectToAction("List", "Element");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(nameof(List));
            }
            // Recuperer l'element a modifier 
            ElementDto? elementToUpdate = await _elementService.GetElementByIdAsync(id);
            if (elementToUpdate is null)
            {
                return Redirect(nameof(List));
            }
            var model = _mapper.Map<UpdateElementVM>(elementToUpdate);
            model.VaultsList = await _vaultService.GetVaultsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateElementVM model)
        {
            if (!ModelState.IsValid)
            {
                model.VaultsList = await _vaultService.GetVaultsAsync();
                return View(model);
            }
            await _elementService.UpdateElementAsync(model);
            return RedirectToAction("List", "Element");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(nameof(List));
            }

            ElementDto? elementToDelete = await _elementService.GetElementByIdAsync(id);
            if (elementToDelete is null)
            {
                return NotFound();
            }
            var model = _mapper.Map<DeleteElementVM>(elementToDelete);
            model.VaultName = elementToDelete.Vault.Label;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteElementVM model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect(nameof(List));
            }
            await _elementService.DeleteElementAsync(model.Id);
            return RedirectToAction("List", "Element");
        }

        [HttpGet]
        public async Task<IActionResult> CheckPinCode(string pinCodePrama,string elementId)
        {
            var myPinCode = HttpContext.Session.GetString("PinCode");
            if(myPinCode != pinCodePrama)
            {
                return Ok(new { isValid = false, message = "Your Pin Code is InCorrect" });
            }
            var element = await _elementService.GetElementByIdAsync(elementId);
            if (element is null)
            {
                return Ok(new { isValid = false, message = "The Element is Not Found" });
            }
            return Ok(new { isValid = true, password = element.Password });
        }
    }
}