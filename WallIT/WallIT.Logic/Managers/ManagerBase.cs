using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using WallIT.DataAccess.Entities.Base;
using WallIT.Shared.DTOs.Base;
using WallIT.Shared.Interfaces.DomainModel.DTO;
using WallIT.Shared.Interfaces.DomainModel.Entity;
using WallIT.Shared.Interfaces.Managers;
using WallIT.Shared.Interfaces.UnitOfWork;
using WallIT.Shared.Transaction;

namespace WallIT.Logic.Managers
{
    public abstract class ManagerBase<TEntity, TDTO> : IManager<TEntity, TDTO>, IDisposable
        where TEntity : EntityBase, IEntity, new()
        where TDTO : DTOBase, IDTO, new()
    {
        protected readonly ISession _session;
        protected readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ManagerBase(ISession session, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _session = session;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            ManageTransaction();

            var isNew = dto.Id == 0;

            var entity = _mapper.Map(dto, isNew
                ? (TEntity)Activator.CreateInstance(typeof(TEntity))
                : _session.Get<TEntity>(dto.Id));

            LoadReferences(entity);

            var result = ValidateSaving(entity);

            OnSaving(entity);

            if (result.Succeeded)
            {
                try
                {
                    if (isNew)
                        _session.Save(entity);

                    result.Id = entity.Id;

                    AfterSaving(entity);
                }
                catch (Exception ex)
                {
                    // TODO logging
                    result.ErrorMessages.Add(new TransactionErrorMessage
                    {
                        IsPublic = false,
                        Message = ex.Message
                    });
                    result.Succeeded = false;
                }
            }

            HandleTransactionErrors(result);

            return result;
        }

        public void Dispose()
        {
            if (_unitOfWork.IsManagedTransaction == false && _session.Transaction.IsActive)
            {
                _session.Transaction.Commit();
                _session.Clear();
            }
        }

        #endregion Interface implementation

        #region Virtual methods

        protected virtual TransactionResult ValidateSaving(TEntity entity)
        {
            return new TransactionResult { Succeeded = true };
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

        private void ManageTransaction()
        {
            if (_unitOfWork.IsManagedTransaction == false)
                _session.Transaction.Begin();
        }

        private void LoadReferences(TEntity entity)
        {
            var referenceProperties = typeof(TEntity).GetProperties().Where(x => typeof(IEntity).IsAssignableFrom(x.PropertyType)).ToArray();
            foreach (var property in referenceProperties)
            {
                var propValue = property.GetValue(entity);
                if (propValue != null)
                {
                    var id = propValue.GetType().GetProperty("Id").GetValue(propValue);
                    var referencedEntity = _session.Load(property.PropertyType, id);

                    property.SetValue(entity, referencedEntity);
                }
                else
                    property.SetValue(entity, null);
            }
        }

        #endregion Private methods
    }
}
