using System;


namespace WallIT.Common.Interfaces
{
    public interface ILogicalDeletable 
    { 
        bool IsDeleted { get; set; }
        DateTime? DeletionDateUTC { get; set; } 
    }
}
