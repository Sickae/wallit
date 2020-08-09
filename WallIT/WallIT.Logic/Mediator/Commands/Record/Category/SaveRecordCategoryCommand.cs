using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WallIT.Logic.DTOs;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Commands
{
    public class SaveRecordCategoryCommand : IRequest<ActionResult>
    {
        public RecordCategoryDTO RecordCategory { get; set; }
    }
}
