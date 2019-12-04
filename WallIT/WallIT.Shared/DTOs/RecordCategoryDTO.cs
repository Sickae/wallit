using System;
using WallIT.Shared.DTOs.Base;

namespace WallIT.Shared.DTOs
{
    public class RecordCategoryDTO : DTOBase
    {
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        public RecordCategoryDTO ParentCategory { get; set; }

        public DateTime? LastUsedUTC { get; set; }
    }
}
