using MediatR;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Queries
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public int UserId { get; set; }
    }
}
