using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WallIT.Logic.DTOs;

namespace WallIT.Logic.Mediator.Commands
{
    public class DeleteRecordTemplateCommand : IRequest<ActionResult>
    {
        public int Id { get; set; }
    }
}
