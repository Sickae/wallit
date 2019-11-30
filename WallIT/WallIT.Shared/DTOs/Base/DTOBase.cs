using System;

namespace WallIT.Shared.DTOs.Base
{
    public abstract class DTOBase
    {
        public int Id { get; set; }

        public DateTime CreationDateUTC { get; set; }

        public DateTime ModificationDateUTC { get; set; }

        protected DTOBase()
        {
            CreationDateUTC = DateTime.UtcNow;
            ModificationDateUTC = DateTime.UtcNow;
        }
    }
}
