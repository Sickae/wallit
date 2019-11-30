using AutoMapper;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using WallIT.Shared.Interfaces.DomainModel.DTO;
using WallIT.Shared.Interfaces.DomainModel.Entity;
using WallIT.Shared.Interfaces.Repositories;

namespace WallIT.Logic.Repositories
{
    public abstract class RepositoryBase<TEntity, TDTO> : IRepository<TEntity, TDTO>
        where TEntity : IEntity
        where TDTO : IDTO
    {
        protected readonly ISession _session;
        private readonly IMapper _mapper;

        public RepositoryBase(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        public TDTO Get(int id)
        {
            var entity = _session.Get<TEntity>(id);
            var dto = _mapper.Map<TDTO>(entity);
            return dto;
        }

        public TDTO[] Get(IEnumerable<int> ids)
        {
            var entities = new List<TEntity>();

            foreach (var id in ids)
            {
                var entity = _session.Get<TEntity>(id);
                if (entity != null)
                    entities.Add(entity);
            }

            var dtos = _mapper.Map<IList<TDTO>>(entities);
            return dtos.ToArray();
        }
    }
}
