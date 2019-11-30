using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Repositories
{
    public class CreditCardRepository : RepositoryBase<CreditCardEntity, CreditCardDTO>, ICreditCardRepository
    {
        public CreditCardRepository(ISession session, IMapper mapper) : base(session, mapper)
        { }
    }
}
