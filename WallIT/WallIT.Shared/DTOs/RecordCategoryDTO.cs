using System;
using WallIT.Shared.DTOs.Base;

namespace WallIT.Shared.DTOs
{
    public class RecordCategoryDTO : DTOBase
    {
        public virtual string Name { get; set; }
        //public virtual ParentCategory ParentCategory { get; set; }
        //public virtual SubCategory SubCategory { get; set; }
        public virtual DateTime? LastUsedUTC { get; set; }
    }
}
