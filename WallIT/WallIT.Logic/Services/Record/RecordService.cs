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
    public class RecordService
    {
        private readonly IMediator _mediator;

        public RecordService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ActionResult> EditRecord(RecordDTO record, int UserId)
        {
            var result = new ActionResult();
            
            var query = new GetAccountByAccountAndUserId
            {
                 AccountId = record.AccountId.Value,
                 UserId = UserId
            };
            var account = await _mediator.Send(query);
            if (account == null)
            {
                result.Suceeded = false;
                result.ErrorMessages.Add("You don't have the right to edit this!");
                return result;
            }
            else
            {
                var command = new EditRecordCommand
                {
                    Record = record
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
        public async Task<ActionResult> DeleteRecord(RecordDTO record, int UserId)
        {
            var result = new ActionResult();
            var query = new GetAccountByAccountAndUserId
            {
                AccountId = record.AccountId.Value,
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
                var command = new DeleteRecordCommand
                {
                    Id = record.Id
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
