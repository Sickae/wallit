using System;
using WallIT.DataAccess.Entities.Base;

namespace WallIT.DataAccess.Entities
{
    public class RecordCategoryEntity : EntityBase
    {
        public virtual string Name { get; set; }
        //public virtual ParentCategory ParentCategory { get; set; }
        //public virtual SubCategory SubCategory { get; set; }
        public virtual DateTime? LastUsedUTC { get; set; }
    }
}
