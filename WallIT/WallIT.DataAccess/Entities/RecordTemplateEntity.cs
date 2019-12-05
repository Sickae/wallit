﻿using WallIT.DataAccess.Entities.Base;

namespace WallIT.DataAccess.Entities
{
    public class RecordTemplateEntity : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual RecordCategoryEntity RecordCategory { get; set; }
        public virtual AccountEntity Account { get; set; }
        public virtual double Amount { get; set; }
    }
}
