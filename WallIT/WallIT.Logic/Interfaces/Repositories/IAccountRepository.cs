using System;
using System.Collections.Generic;
using System.Text;
using WallIT.DataAccess.Entities;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.DomainModel.DTO;
using WallIT.Shared.Interfaces.Repositories;

namespace WallIT.Logic.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<AccountEntity, AccountDTO>
    {
        AccountDTO GetAccountByAccountAndUserId(int accountId, int UserId);
    }
}
