using MediatR;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Queries
{
    public class GetAccountListQuery : IRequest<AccountDTO[]>
    {
    }
}
