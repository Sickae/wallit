using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WallIT.Logic.DTOs;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Commands
{
    public class AccountSaveCommand : IRequest<ActionResult>
    {
        public AccountDTO account { get; set; }
    }
}
