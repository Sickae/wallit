using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Repositories
{
    public class RecordCategoryRepository : RepositoryBase<RecordCategoryEntity, RecordCategoryDTO>, IRecordCategoryRepository
    {
        public RecordCategoryRepository(ISession session, IMapper mapper) : base(session, mapper)
        { }
    }
}
