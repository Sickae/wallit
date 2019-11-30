﻿using System;
using WallIT.Shared.Interfaces.DomainModel.DTO;

namespace WallIT.Shared.DTOs.Base
{
    public abstract class DTOBase : IDTO
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
