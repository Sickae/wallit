using System;
using WallIT.DataAccess.Entities.Base;

namespace WallIT.DataAccess.Entities
{
    public class RecordCategoryEntity : EntityBase
    {
        public virtual string Name { get; set; }

        public virtual RecordCategoryEntity ParentCategory { get; set; }

        public virtual DateTime? LastUsedUTC { get; set; }
        //public virtual AccountEntity AccountEntity { get; set; }
    }
}
