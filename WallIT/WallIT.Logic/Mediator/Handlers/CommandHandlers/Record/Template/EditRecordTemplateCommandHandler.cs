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
    public class EditRecordTemplateCommandHandler : IRequestHandler<EditRecordTemplateCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public EditRecordTemplateCommandHandler(ISession session, IUnitOfWork unitOfWork)
        {
            _session = session;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Handle(EditRecordTemplateCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _unitOfWork.BeginTransaction();
            var category = _session.Load<RecordCategoryEntity>(request.RecordTemplate.RecordCategoryId);
            var account = _session.Load<AccountEntity>(request.RecordTemplate.AccountId);
            using (var trans = _session.BeginTransaction())
            {
                var record = new RecordTemplateEntity
                {
                    Account = account,
                    RecordCategory = category,
                    Amount = request.RecordTemplate.Amount,
                    Name = request.RecordTemplate.Name,
                    ModificationDateUTC = DateTime.UtcNow
                };
                _session.Update(record);
                trans.Commit();
            }

            return new ActionResult { Suceeded = true };
        }
    }
}
