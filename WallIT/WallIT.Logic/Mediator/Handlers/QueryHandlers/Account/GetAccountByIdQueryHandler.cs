using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Logic.Mediator.Queries;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Handlers.QueryHandlers
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDTO>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountByIdQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<AccountDTO> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var account = _accountRepository.Get(request.Id);

            return Task.FromResult(account);
        }
    }
}
