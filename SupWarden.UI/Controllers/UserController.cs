using AutoMapper;
using SupWarden.Dto.Dtos.User;
using SupWarden.UI.Services.Contracts;
using SupWarden.UI.ViewModels.User;

namespace SupWarden.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }



        // GET: User/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

         
            var result = await _userService.AddUserAsync(model);
            if (result)
            {
                ViewData["IsCreated"] = true;
                return RedirectToAction("Profile", "User");
            }

            ModelState.AddModelError("", "Failed to create user. Please try again.");
            return View(model);
        }
    



    // GET: User/Edit

    [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Profile));
            }

            // Récupérer l'utilisateur à modifier
            UserDto? userToUpdate = await _userService.GetUserByIdAsync(id);
            if (userToUpdate is null)
            {
                return RedirectToAction(nameof(Profile));
            }

            var model = _mapper.Map<UpdateProfileVM>(userToUpdate);
            return View(model);
        }

        // POST: User/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfileVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var userId = model.Id;
            var userDto = _mapper.Map<UserDto>(model);

            // Mettre à jour l'utilisateur en utilisant l'ID et le DTO de l'utilisateur
            await _userService.UpdateUserAsync(userId, userDto);

            return RedirectToAction("Profile", "User");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Appel du service pour changer le mot de passe
            var success = await _userService.ChangePasswordAsync(User!.Identity!.Name!, model.CurrentPassword, model.NewPassword);
            if (success)
            {
                ViewData["IsPasswordChanged"] = true;
                return RedirectToAction("Profile", "User");
            }

            ModelState.AddModelError("", "Failed to change password.");
            return View(model);
        }

        [HttpGet]
        public IActionResult SetPinCode()
        {
            ViewData["pinCode"] = HttpContext.Session.GetString("PinCode");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetPinCode(SetPinCodeVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var success = await _userService.SetPinCodeAsync(model.PinCode);
            if (success)
            {
                ViewData["PinCodeUpdated"] = true;
                HttpContext.Session.SetString("PinCode", model.PinCode.ToString());
                ViewData["pinCode"] = HttpContext.Session.GetString("PinCode");
                return View(new SetPinCodeVM());
            }

            ModelState.AddModelError("", "Failed to set PIN code.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckPinCode(string pinCode)
        {
            var success = await _userService.VerifyPinCodeAsync(User!.Identity!.Name!, pinCode);
            if (success)
            {
                return Ok(); // Autoriser l'accès
            }
            return Unauthorized(); // Refuser l'accès
        }
    
    
        }
}