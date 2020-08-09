using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallIT.Logic.DTOs;
using WallIT.Logic.Mediator.Commands;
using WallIT.Logic.Mediator.Queries;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Services
{
    public class AccountService
    {
        private readonly IMediator _mediator;

        public AccountService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ActionResult> EditAccount(AccountDTO account,int AccountId, int UserId)
        {
            var result = new ActionResult();
            account.UserId = UserId;
            var query = new GetAccountByAccountAndUserId
            {
                AccountId = AccountId,
                UserId = UserId
            };
            var QueryResult = await _mediator.Send(query);
            if (QueryResult == null)
            {
                result.Suceeded = false;
                result.ErrorMessages.Add("You don't have the right to edit this!");
                return result;
            }
            else
            {
                var command = new EditAccountCommand
                {
                    account = account
                };
                var CommandResult = await _mediator.Send(command);
                if (CommandResult.Suceeded)
                {
                    result.Suceeded = true;
                }
                else
                {
                    foreach (var msg in result.ErrorMessages)
                        result.ErrorMessages.Add(msg);
                }
            }
                return result;
        }
        public async Task<ActionResult> DeleteAccount(int AccountId, int UserId)
        {
            var result = new ActionResult();
            var query = new GetAccountByAccountAndUserId
            {
                AccountId = AccountId,
                UserId = UserId
            };
            var QueryResult = await _mediator.Send(query);
            if (QueryResult == null)
            {
                result.Suceeded = false;
                result.ErrorMessages.Add("You don't have the right to delete this!");
                return result;
            }
            else
            {
                var command = new DeleteAccountCommand
                {
                    Id = QueryResult.Id
                };
                var CommandResult = await _mediator.Send(command);
                if (CommandResult.Suceeded)
                {
                    result.Suceeded = true;
                }
                else
                {
                    foreach (var msg in result.ErrorMessages)
                        result.ErrorMessages.Add(msg);
                }

            }
            return result;
        }
    }
}
