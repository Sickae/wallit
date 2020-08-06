using MediatR;
using System.Threading.Tasks;
using WallIT.Logic.DTOs;
using WallIT.Logic.Mediator.Commands;
using WallIT.Logic.Mediator.Queries;

namespace WallIT.Logic.Services
{
    public class DeleteAccountService
    {
        private readonly IMediator _mediator;

        public DeleteAccountService(IMediator mediator)
        {
            _mediator = mediator;
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
                    AccountId = QueryResult.Id
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
