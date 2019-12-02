using WallIT.DataAccess.Entities;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.Managers;

namespace WallIT.Logic.Interfaces.Managers
{
    public interface IUserManager : IManager<UserEntity, UserDTO>
    { }
}
