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
using WallIT.Shared.Interfaces.UnitOfWork;

namespace WallIT.Logic.Mediator.Handlers.CommandHandlers
{
    public class EditRecordCategoryCommandHandler : IRequestHandler<EditRecordCategoryCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public EditRecordCategoryCommandHandler(ISession session, IUnitOfWork unitOfWork)
        {
            _session = session;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Handle(EditRecordCategoryCommand request, CancellationToken cancellationToken)
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
                    ModificationDateUTC = DateTime.UtcNow
                };
                _session.Update(record);
                trans.Commit();
            }

            return new ActionResult { Suceeded = true };
        }
    }
}
