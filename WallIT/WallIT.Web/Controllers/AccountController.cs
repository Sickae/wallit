using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Mediator.Queries;

namespace WallIT.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}