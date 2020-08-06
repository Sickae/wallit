using MediatR;
using WallIT.Logic.DTOs;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Commands
{
    public class EditAccountCommand : IRequest<ActionResult>
    {
        public AccountDTO account { get; set; }
    }
}
