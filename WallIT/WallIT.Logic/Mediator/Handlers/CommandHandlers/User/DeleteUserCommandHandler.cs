using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WallIT.Logic.Interfaces.Managers;
using WallIT.Logic.Mediator.Commands;
using WallIT.Shared.Transaction;

namespace WallIT.Logic.Mediator.Handlers.CommandHandlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, TransactionResult>
    {
        private readonly IUserManager _userManager;

        public DeleteUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public Task<TransactionResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = _userManager.Delete(request.UserId);
            return Task.FromResult(result);
        }
    }
}
