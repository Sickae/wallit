using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using WallIT.Shared.Interfaces.DomainModel.DTO;
using WallIT.Shared.Interfaces.DomainModel.Entity;
using WallIT.Shared.Interfaces.Managers;
using WallIT.Shared.Transaction;

namespace WallIT.Logic.Managers
{
    public abstract class ManagerBase<TEntity, TDTO> : IManagerBase<TEntity, TDTO>
        where TEntity : IEntity
        where TDTO : IDTO
    {
        protected readonly ISession _session;
        protected readonly IMapper _mapper;

        public ManagerBase(ISession session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        #region Interface implementation

        public TransactionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TransactionResult Delete(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public TransactionResult Save(TDTO dto)
        {
            var isNew = dto.Id == 0;

            var entity = _mapper.Map(dto, isNew
                ? (TEntity)Activator.CreateInstance(typeof(TEntity))
                : _session.Get<TEntity>(dto.Id));

            var result = ValidateSaving(entity);

            if (result.IsSuccess)
            {
                try
                {
                    if (isNew)
                        _session.Save(entity);

                    result.Id = entity.Id;
                }
                catch (Exception ex)
                {
                    // TODO logging
                    result.ErrorMessages.Add(new TransactionErrorMessage
                    {
                        IsPublic = false,
                        Message = ex.Message
                    });
                    result.IsSuccess = false;
                }
            }

            HandleTransactionErrors(result);

            return result;
        }

        #endregion Interface implementation

        #region Virtual methods

        protected virtual TransactionResult ValidateSaving(TEntity entity)
        {
            return new TransactionResult();
        }

        protected virtual void OnSaving(TEntity entity)
        { }

        protected virtual void AfterSaving(TEntity entity)
        { }

        protected virtual void OnDeleting(TEntity entity)
        { }

        protected virtual void AfterDeleting(TEntity entity)
        { }

        #endregion Virtual methods

        #region Private methods

        private void HandleTransactionErrors(TransactionResult result)
        {
            // TODO handle non-public errors
            var errorsToRemove = result.ErrorMessages.Where(x => !x.IsPublic).ToList();

            foreach (var error in errorsToRemove)
                result.ErrorMessages.Remove(error);
        }

        #endregion Private methods






        // TODO REFACTOR!!!!!
        //public static TEntity ToEntity<DTO, T>(this DTO dtoModelFrom, ISession session, T entityTo, IMapper mapper) where T : IEntity where DTO : IDTO
        //{
        //    var entityType = typeof(T);
        //    var dtoType = typeof(DTO);
        //    var propertyMappings = mapper.ConfigurationProvider.FindTypeMapFor(dtoType, entityType)?.PropertyMaps;

        //    if (propertyMappings == null)
        //    {
        //        //Log.Logger.Error(string.Format("Hiányzó mapping: {0} --> {1}", dtoType.Name, entityType.Name));
        //        throw new MappingException(string.Format("Hiányzó mapping: {0} --> {1}", dtoType.Name, entityType.Name));
        //    }

        //    if (entityTo == null)
        //    {
        //        entityTo = (T)Activator.CreateInstance(entityType);
        //    }

        //    mapper.Map(dtoModelFrom, entityTo); // átmappeljük az alap propertyket

        //    foreach (var dtoProp in dtoType.GetProperties())
        //    {
        //        // csak az Id-ra végződő property-k, vagy a ID listák
        //        if (!(dtoProp.Name.EndsWith("Id") ||
        //              dtoProp.Name.EndsWith("Ids") ||
        //              dtoProp.Name.EndsWith("s") ||
        //              dtoProp.Name.EndsWith("ies")))
        //        {
        //            continue;
        //        }


        //        //if (Attribute.IsDefined(dtoProp, typeof(IgnoreMappingAttribute)))
        //        //{
        //        //    continue;
        //        //}

        //        foreach (var entityProp in entityType.GetProperties()) // és az eredeti entitás entity-s property-jei
        //        {
        //            // újratöltjük az összes Entity-s property-t az ID-k alapján (pl. dtoModelFrom.RegionId --> entityTo.Region kitöltésre kerül
        //            if (dtoProp.Name == entityProp.Name + "Id" &&
        //                typeof(EntityBase).IsAssignableFrom(entityProp.PropertyType) &&
        //                (typeof(int) == dtoProp.PropertyType || typeof(int?) == dtoProp.PropertyType))
        //            {
        //                // extra írhatóság ellenőrzés
        //                if (entityProp.CanWrite && entityProp.GetSetMethod(true).IsPublic)
        //                {
        //                    var newEntityId = (int?)dtoProp.GetValue(dtoModelFrom) ?? 0;
        //                    var value = newEntityId > 0 ? session.Load(entityProp.PropertyType, newEntityId) : null;
        //                    entityProp.SetValue(entityTo, value);
        //                }

        //                break; // megtaláltuk a párját
        //            }

        //            // összes lista property újratöltése
        //            var nameCheck = (dtoProp.Name == entityProp.Name + "Ids") // Object <=> ObjectIds
        //                            || (entityProp.Name.EndsWith("s") // Objects <=> ObjectIds
        //                                && dtoProp.Name == entityProp.Name.Remove(entityProp.Name.Length - 1) + "Ids")
        //                            || (entityProp.Name.EndsWith("ies") // Entities <=> EntityIds
        //                                && dtoProp.Name == entityProp.Name.Remove(entityProp.Name.Length - 3) + "yIds");
        //            if (nameCheck && // az elnevezés összekapcsolható
        //                dtoProp.PropertyType.IsGenericType && // a dtoProp IList<int> ?
        //                dtoProp.PropertyType.GetGenericTypeDefinition() == typeof(IList<>) &&
        //                dtoProp.PropertyType.GetGenericArguments()[0] == typeof(int) &&
        //                entityProp.PropertyType.IsGenericType && // az entityProp IList<Entity> ?
        //                (entityProp.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>)
        //                 || entityProp.PropertyType.GetGenericTypeDefinition() == typeof(IList<>)) &&
        //                typeof(EntityBase).IsAssignableFrom(entityProp.PropertyType.GetGenericArguments()[0]))
        //            {
        //                // extra írhatóság ellenőrzés (invert)
        //                if (!(entityProp.CanWrite && entityProp.GetSetMethod(true).IsPublic))
        //                {
        //                    break;
        //                }

        //                // az IList<Entity>-ből az Entity tényleges típusa
        //                var listEntityType = entityProp.PropertyType.GetGenericArguments()[0];
        //                var newEntityIds = (IList<int>)dtoProp.GetValue(dtoModelFrom);

        //                if (newEntityIds != null)
        //                {
        //                    // List<Entity> példány létrehozása
        //                    var newList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(listEntityType));

        //                    foreach (var id in newEntityIds)
        //                    {
        //                        var value = session.Load(listEntityType, id);
        //                        newList.Add(value);
        //                    }

        //                    entityProp.SetValue(entityTo, newList);
        //                    break; // megtaláltuk a párját
        //                }

        //                entityProp.SetValue(entityTo, null);
        //                break; // megtaláltuk a párját
        //            }
        //        }
        //    }

        //    if (entityTo.Id == 0 && entityTo is ILogicalDeletableEntity)
        //        ((ILogicalDeletableEntity)entityTo).Active = true;

        //    if (entityTo.Id == 0 && entityTo is IMkikEntity)
        //    {
        //        ((IMkikEntity)entityTo).Created = DateTime.Now;
        //        if (((IMkikEntity)entityTo).CreatedBy == null)
        //            ((IMkikEntity)entityTo).CreatedBy = session.Load<User>(appContext.UserId);
        //    }

        //    return entityTo;
        //}
    }
}
