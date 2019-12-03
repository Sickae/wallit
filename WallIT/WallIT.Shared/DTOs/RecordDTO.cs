using System;
using WallIT.Shared.DTOs.Base;
using WallIT.Shared.Enums;

namespace WallIT.Shared.DTOs
{
    public class RecordDTO : DTOBase
    {
        //public RecordCategory RecordCategory { get; set; }

        //public Account Account { get; set; }

        public double Amount { get; set; }

        public Currency Currency { get; set; }

        public DateTime TransactionDateUTC { get; set; }
    }
}
