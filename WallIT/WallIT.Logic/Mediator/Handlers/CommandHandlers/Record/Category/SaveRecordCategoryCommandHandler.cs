using MediatR;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WallIT.DataAccess.Entities;
using WallIT.Logic.DTOs;
using WallIT.Logic.Mediator.Commands;
using WallIT.Shared.DTOs;
using WallIT.Shared.Interfaces.UnitOfWork;

namespace WallIT.Logic.Mediator.Handlers.CommandHandlers
{
    public class SaveRecordCategoryCommandHandler : IRequestHandler<SaveRecordCategoryCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public SaveRecordCategoryCommandHandler(ISession session, IUnitOfWork unitOfWork)
        {
            _session = session;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Handle(SaveRecordCategoryCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _unitOfWork.BeginTransaction();
            var ParentCategory = _session.Load<RecordCategoryEntity>(request.RecordCategory.ParentCategoryId);
            using (var trans = _session.BeginTransaction())
            {
                var record = new RecordCategoryEntity
                {
                    Name = request.RecordCategory.Name,
                    LastUsedUTC = DateTime.UtcNow,
                    ParentCategory = ParentCategory,
                    CreationDateUTC = DateTime.UtcNow
                };
                _session.Save(record);
                trans.Commit();
            }

            return new ActionResult { Suceeded = true };
        }
    }
}
