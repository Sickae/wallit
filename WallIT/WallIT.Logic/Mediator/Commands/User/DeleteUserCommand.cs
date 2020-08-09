using MediatR;
using WallIT.Shared.Transaction;

namespace WallIT.Logic.Mediator.Commands
{
    public class DeleteUserCommand : IRequest<TransactionResult>
    {
        public int UserId { get; set; }
    }
}
