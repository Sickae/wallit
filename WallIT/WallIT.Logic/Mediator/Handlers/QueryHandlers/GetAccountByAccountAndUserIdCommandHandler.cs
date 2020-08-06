using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Logic.Mediator.Queries;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Handlers.QueryHandlers
{
    public class GetAccountByAccountAndUserIdCommandHandler : IRequestHandler<GetAccountByAccountAndUserId, AccountDTO>
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountByAccountAndUserIdCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<AccountDTO> Handle(GetAccountByAccountAndUserId request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var account = _accountRepository.GetAll();
            return Task.FromResult(account);
        }
    }
}
