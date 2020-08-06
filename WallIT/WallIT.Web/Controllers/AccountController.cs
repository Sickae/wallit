using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Identity;
using WallIT.Logic.Mediator.Commands;
using WallIT.Logic.Mediator.Queries;
using WallIT.Shared.DTOs;

namespace WallIT.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppIdentityUser> _userManager;
        public AccountController(IMediator mediator, UserManager<AppIdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
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
            var command = new AccountSaveCommand
            {
                account = model
            };
            await _mediator.Send(command);
            return Json(true);
        }
    }
}