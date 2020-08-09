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
    public class RecordCategoryService
    {
        private readonly IMediator _mediator;

        public RecordCategoryService(IMediator mediator)
        {
            _mediator = mediator;
        }
        /*public async Task<ActionResult> EditRecordCategory(RecordCategoryDTO recordcategory, int UserId)
        {
            var result = new ActionResult();
            var query = new GetAccountByAccountAndUserId
            {
                AccountId = recordcategory.AccountId.Value,
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
                var command = new EditRecordCategoryCommand
                {
                    RecordCategory = recordcategory
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
        }*/
    }
}
