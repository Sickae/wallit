using WallIT.Shared.DTOs;
using WallIT.DataAccess.Entities;
using WallIT.Shared.Interfaces.Managers;

namespace WallIT.Logic.Interfaces.Managers
{
    public interface IAccountManager : IManager<AccountEntity, AccountDTO>
    {}
}
