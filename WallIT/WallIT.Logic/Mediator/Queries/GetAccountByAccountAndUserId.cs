using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Mediator.Queries
{
    public class GetAccountByAccountAndUserId : IRequest<AccountDTO>
    {
        public int UserId { get; set; }
        public int AccountId { get; set; }
    }
}
