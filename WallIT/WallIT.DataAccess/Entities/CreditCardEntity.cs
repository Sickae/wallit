using WallIT.Common.Attributes;
using WallIT.DataAccess.Entities.Base;

namespace WallIT.DataAccess.Entities
{
    public class CreditCardEntity : EntityBase
    {
        [NotNull]
        public virtual string Name { get; set; }
    }
}
