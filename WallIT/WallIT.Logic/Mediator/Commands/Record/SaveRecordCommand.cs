using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WallIT.Logic.DTOs;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Commands
{
    public class SaveRecordCommand : IRequest<ActionResult>
    {
        public RecordDTO record { get; set }
    }
}
