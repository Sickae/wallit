using MediatR;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Queries
{
    public class GetAccountByIdQuery : IRequest<AccountDTO>
    {
        public int Id { get; set; }
    }
}
