using System;
using System.Collections.Generic;
using System.Text;
using WallIT.DataAccess.Entities;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.Managers;

namespace WallIT.Logic.Interfaces.Managers
{
    public interface IRecordCategoryManager : IManager<RecordCategoryEntity, RecordCategoryDTO>
    {}
}
