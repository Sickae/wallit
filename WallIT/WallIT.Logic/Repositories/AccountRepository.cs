using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.DomainModel.DTO;

namespace WallIT.Logic.Repositories
{
    public class AccountRepository : RepositoryBase<AccountEntity, AccountDTO>, IAccountRepository
    {
        public AccountRepository(ISession session, IMapper mapper) : base(session, mapper)
        { }

        public IDTO GetAccountByAccountAndUserId(int accountId, int UserId)
        {
            var result = _session.QueryOver<AccountEntity>()
                .Where(x => x.Id == accountId)
                .Where(x => x.User.Id == UserId);

            if (result == null)
                return null;

            var dto = _mapper.Map<AccountDTO>(result);
            return dto;
        }
    }
}
