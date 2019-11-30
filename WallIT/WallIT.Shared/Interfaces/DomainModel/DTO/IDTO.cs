using System;

namespace WallIT.Shared.Interfaces.DomainModel.DTO
{
    public interface IDTO
    {
        int Id { get; }

        DateTime CreationDateUTC { get; }

        DateTime ModificationDateUTC { get; }
    }
}
