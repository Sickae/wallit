using AutoMapper;
using NHibernate;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Managers;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.UnitOfWork;

namespace WallIT.Logic.Managers
{
    public class UserClaimManager : ManagerBase<UserClaimEntity, UserClaimDTO>, IUserClaimManager
    {
        public UserClaimManager(ISession session, IMapper mapper, IUnitOfWork unitOfWork) : base(session, mapper, unitOfWork)
        { }
    }
}
