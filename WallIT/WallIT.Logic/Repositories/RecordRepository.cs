using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Repositories
{
    public class RecordRepository : RepositoryBase<RecordEntity, RecordDTO>, IRecordRepository
    {
        public RecordRepository(ISession session, IMapper mapper) : base(session, mapper)
        { }
    }
}
