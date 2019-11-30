using System.Collections.Generic;
using WallIT.Shared.Interfaces.DomainModel.DTO;
using WallIT.Shared.Interfaces.DomainModel.Entity;

namespace WallIT.Shared.Interfaces.Repositories
{
    public interface IRepository<TEntity, TDTO>
        where TEntity : IEntity
        where TDTO : IDTO
    {
        TDTO Get(int id);

        TDTO[] Get(IEnumerable<int> ids);
    }
}
