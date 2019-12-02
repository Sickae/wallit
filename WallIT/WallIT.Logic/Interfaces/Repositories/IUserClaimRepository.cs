using WallIT.DataAccess.Entities;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.Repositories;

namespace WallIT.Logic.Interfaces.Repositories
{
    public interface IUserClaimRepository : IRepository<UserClaimEntity, UserClaimDTO>
    {
        UserClaimDTO[] GetByUserId(int userId);

        UserClaimDTO[] GetSpecificClaimsByUserId(int userId, string claimType = null, string claimValue = null);
    }
}
