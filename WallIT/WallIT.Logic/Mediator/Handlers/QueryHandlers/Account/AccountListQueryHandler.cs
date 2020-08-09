using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Logic.Mediator.Queries;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Handlers.QueryHandlers
{
    public class AccountListQueryHandler : IRequestHandler<GetAccountListQuery, AccountDTO[]>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountListQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public Task<AccountDTO[]> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var account = _accountRepository.GetAll();
            return Task.FromResult(account);
        }
    }
}
