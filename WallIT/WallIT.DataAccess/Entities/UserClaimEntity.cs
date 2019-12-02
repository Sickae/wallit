using WallIT.DataAccess.Entities.Base;

namespace WallIT.DataAccess.Entities
{
    public class UserClaimEntity : EntityBase
    {
        public virtual UserEntity User { get; set; }

        public virtual string ClaimType { get; set; }

        public virtual string ClaimValue { get; set; }
    }
}
