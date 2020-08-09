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
    public class RecordTemplateService
    {
        private readonly IMediator _mediator;

        public RecordTemplateService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ActionResult> EditRecordTemplate(RecordTemplateDTO recordtemplate,int UserId)
        {
            var result = new ActionResult();

            var query = new GetAccountByAccountAndUserId
            {
                AccountId = recordtemplate.AccountId.Value,
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
                var command = new EditRecordTemplateCommand
                {
                    RecordTemplate = recordtemplate
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
        public async Task<ActionResult> DeleteRecordTemplate(RecordTemplateDTO recordtemplate, int UserId)
        {
            var result = new ActionResult();
            var query = new GetAccountByAccountAndUserId
            {
                AccountId = recordtemplate.AccountId.Value,
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
                var command = new DeleteRecordTemplateCommand
                {
                    Id = recordtemplate.Id
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
