using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Identity;
using WallIT.Logic.Mediator.Commands;
using WallIT.Logic.Mediator.Queries;
using WallIT.Logic.Services;
using WallIT.Shared.DTOs;

namespace WallIT.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly AccountService _editAccountService; 
        public AccountController(IMediator mediator, UserManager<AppIdentityUser> userManager, AccountService editAccountService, IStringLocalizer<AccountController> localizer)
        {
            _mediator = mediator;
            _userManager = userManager;
            _editAccountService = editAccountService;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var query = new GetAccountByIdQuery { Id = id };
            var account = await _mediator.Send(query);

            return View(account);
        }
        public async Task<IActionResult> List() 
        {
            var query = new GetAccountListQuery();
            var account = await _mediator.Send(query);
            return Json(account);
        }
        [Authorize]
        public async Task<IActionResult> SaveAccount(AccountDTO model) 
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            model.UserId = user.Id;
            var command = new SaveAccountCommand
            {
                account = model
            };
            var CommandResult = await _mediator.Send(command);
            if (!CommandResult.Suceeded)
            {
                foreach (var msg in CommandResult.ErrorMessages)
                    ModelState.AddModelError("", _localizer[msg]);

                return View(model);
            }
            return Json(true);
        }
        [Authorize]
        public async Task<IActionResult> EditAccount(AccountDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return Json(_editAccountService.EditAccount(model, model.Id, user.Id));
        }
        [Authorize]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return Json(_deleteAccountService.DeleteAccount(id, user.Id));
        }
    }
}