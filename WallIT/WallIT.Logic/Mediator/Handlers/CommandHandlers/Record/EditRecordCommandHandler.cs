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
    public class EditRecordCommandHandler : IRequestHandler<EditRecordCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public EditRecordCommandHandler(ISession session, IUnitOfWork unitOfWork)
        {
            _session = session;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Handle(EditRecordCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _unitOfWork.BeginTransaction();
            var category = _session.Load<RecordCategoryEntity>(request.Record.RecordCategoryId);
            var account = _session.Load<AccountEntity>(request.Record.AccountId);
            using (var trans = _session.BeginTransaction())
            {
                var record = new RecordEntity
                {
                    Account = account,
                    RecordCategory = category,
                    Amount = request.Record.Amount,
                    Currency = request.Record.Currency,
                    ModificationDateUTC = DateTime.UtcNow,
                    TransactionDateUTC = DateTime.UtcNow

                };
                _session.Update(record);
                trans.Commit();
            }

            return new ActionResult { Suceeded = true };
        }
    }
}
