using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WallIT.Logic.Identity;
using WallIT.Logic.Mediator.Commands;

namespace WallIT.Logic.Mediator.Handlers.CommandHandlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly AppSignInManager _signInManager;

        public LogoutCommandHandler(AppSignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _signInManager.SignOutAsync();

            return Unit.Value;
        }
    }
}
