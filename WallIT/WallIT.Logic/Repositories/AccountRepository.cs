using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Repositories
{
    public class AccountRepository : RepositoryBase<AccountEntity, AccountDTO>, IAccountRepository
    {
        public AccountRepository(ISession session, IMapper mapper) : base(session, mapper)
        { }
    }
}
