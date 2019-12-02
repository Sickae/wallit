using System.Collections.Generic;
using WallIT.Shared.Interfaces.DomainModel.DTO;
using WallIT.Shared.Interfaces.DomainModel.Entity;
using WallIT.Shared.Transaction;

namespace WallIT.Shared.Interfaces.Managers
{
    public interface IManager<TEntity, TDTO>
        where TEntity : IEntity
        where TDTO : IDTO
    {
        TransactionResult Save(TDTO dto);

        TransactionResult Delete(int id);

        TransactionResult Delete(IEnumerable<int> ids);
    }
}
