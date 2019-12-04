using AutoMapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using WallIT.DataAccess.Entities;
using WallIT.Logic.Interfaces.Managers;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.UnitOfWork;

namespace WallIT.Logic.Managers
{
    public class RecordCategoryManager : ManagerBase<RecordCategoryEntity, RecordCategoryDTO>, IRecordCategoryManager
    {
        public RecordCategoryManager(ISession session, IMapper mapper, IUnitOfWork unitOfWork) : base(session, mapper, unitOfWork)
        { }
    }
}
