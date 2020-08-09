using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WallIT.Logic.Mediator.Queries;

namespace WallIT.Web.Controllers
{
    public class RecordController : Controller
    {
        private readonly IMediator _mediator; 
        public RecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RecordDetails(int id)
        {
            var query = new GetRecordByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            return View(result);
        }
        public async Task<IActionResult> RecordCategoryDetails(int id)
        {
            var query = new GetRecordCategoryByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            return View(result);
        }
    }
}