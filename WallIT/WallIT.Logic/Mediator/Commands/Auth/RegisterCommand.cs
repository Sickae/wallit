using MediatR;
using WallIT.Logic.DTOs;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Commands
{
    public class RegisterCommand : IRequest<ActionResult>
    {
        public UserDTO User { get; set; }

        public string Password { get; set; }
    }
}
