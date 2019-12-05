using WallIT.DataAccess.Entities;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.Repositories;

namespace WallIT.Logic.Interfaces.Repositories
{
    public interface IRecordTemplateRepository : IRepository<RecordTemplateEntity, RecordTemplateDTO>
    {}
}
