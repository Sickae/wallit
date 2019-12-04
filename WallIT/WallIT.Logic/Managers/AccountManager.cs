
using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Managers;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.UnitOfWork;

namespace WallIT.Logic.Managers
{
    public class AccountManager : ManagerBase<AccountEntity, AccountDTO>, IAccountManager
    {
        public AccountManager(ISession session, IMapper mapper, IUnitOfWork unitOfWork) : base(session, mapper, unitOfWork)
        { }
    }
}
