using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WallIT.Logic.Identity;
using WallIT.Shared.Interfaces.UnitOfWork;
using WallIT.Web.Models;

namespace WallIT.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly AppIdentityUserManager _identityUserManager;
        private readonly AppSignInManager _signInManager;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountController(AppIdentityUserManager identityUserManager,
            AppSignInManager signInManager,
            IOptions<IdentityOptions> identityOptions,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _identityUserManager = identityUserManager;
            _signInManager = signInManager;
            _identityOptions = identityOptions;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            SetTitle("Login");

            var model = new LoginModel
            {
                ReturnUrl = returnUrl ?? Url.Content("~/")
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            SetTitle("Login");

            if (ModelState.IsValid)
            {
                _unitOfWork.BeginTransaction();

                var user = await _identityUserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "E-mail or password is incorrect!");
                    return View(model);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    return LocalRedirect(model.ReturnUrl);
                }
                else
                {
                    if (result.IsLockedOut)
                        ModelState.AddModelError("", "Your account is locked out!");
                    else
                        ModelState.AddModelError("", "E-mail or password is incorrect!");
                }

                _unitOfWork.Commit();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult ForgotPassword()
        {
            // TODO finish
            return Json("WIP");
        }

        public IActionResult ChangePassword()
        {
            // TODO finish
            return Json("WIP");
        }

        [HttpGet]
        public IActionResult Register()
        {
            SetTitle("Registration");

            var model = new RegisterModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            SetTitle("Registration");

            if (!ModelState.IsValid)
                return View(model);

            _unitOfWork.BeginTransaction();

            var appUser = _mapper.Map<AppIdentityUser>(model.User);
            var result = await _identityUserManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                var userPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var userId = int.Parse(_identityUserManager.GetUserId(userPrincipal));

                await _signInManager.SignInAsync(appUser, true);

                _unitOfWork.Commit();

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        protected override void FillViewBags()
        {
            base.FillViewBags();

            var pwdOptions = _identityOptions.Value.Password;
            var pwdInfo = new List<string>();

            pwdInfo.Add($"Password must be at least {pwdOptions.RequiredLength} characters long!");
            if (pwdOptions.RequireDigit)
                pwdInfo.Add("Password must contain a digit!");
            if (pwdOptions.RequireLowercase)
                pwdInfo.Add("Password must contain a lowercase letter!");
            if (pwdOptions.RequireUppercase)
                pwdInfo.Add("Password must contain an uppercase letter!");
            if (pwdOptions.RequireNonAlphanumeric)
                pwdInfo.Add("Password must contain a non-alphanumeric character (;,* etc.)!");

            ViewBag.PasswordInfo = pwdInfo;
        }
    }
}