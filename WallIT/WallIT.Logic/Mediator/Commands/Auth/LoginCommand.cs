﻿using MediatR;
using WallIT.Logic.DTOs;

namespace WallIT.Logic.Mediator.Commands
{
    public class LoginCommand : IRequest<ActionResult>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
