﻿using System;
using WallIT.Common.Interfaces;

namespace WallIT.DataAccess.Entities.Base
{
    public abstract class LogicalEntityBase : EntityBase, ILogicalDeletable
    {
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime? DeletionDateUTC { get; set; }
    }
}
