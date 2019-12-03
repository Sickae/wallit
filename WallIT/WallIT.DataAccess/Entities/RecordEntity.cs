using System;
using WallIT.DataAccess.Entities.Base;
using WallIT.Shared.Enums;

namespace WallIT.DataAccess.Entities
{
    public class RecordEntity : EntityBase
    {
        //public virtual RecordCategory RecordCategory { get; set; }

        //public virtual Account Account { get; set; }

        public virtual double Amount { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual DateTime TransactionDateUTC { get; set; }
    }
}
