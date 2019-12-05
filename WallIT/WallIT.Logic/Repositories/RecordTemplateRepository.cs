using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Repositories;
using WallIT.Shared.DTOs;

namespace WallIT.Logic.Repositories
{
    public class RecordTemplateRepository : RepositoryBase<RecordTemplateEntity, RecordTemplateDTO>, IRecordTemplateRepository
    {
        public RecordTemplateRepository(ISession session, IMapper mapper) : base(session, mapper)
        { }
    }
}
