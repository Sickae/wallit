using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Managers;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.UnitOfWork;
using WallIT.Shared.Transaction;

namespace WallIT.Logic.Managers
{
    public class UserManager : ManagerBase<UserEntity, UserDTO>, IUserManager
    {
        public UserManager(ISession session, IMapper mapper, IUnitOfWork unitOfWork) : base(session, mapper, unitOfWork)
        { }

        protected override TransactionResult ValidateSaving(UserEntity entity)
        {
            var result = base.ValidateSaving(entity);
            
            if (string.IsNullOrEmpty(entity.Email))
            {
                result.ErrorMessages.Add(new TransactionErrorMessage
                {
                    IsPublic = true,
                    Message = "Email is required!"
                });
                result.Succeeded = false;
            }

            if (string.IsNullOrEmpty(entity.Name))
            {
                result.ErrorMessages.Add(new TransactionErrorMessage
                {
                    IsPublic = true,
                    Message = "Name is required!"
                });
                result.Succeeded = false;
            }

            return result;
        }
    }
}
