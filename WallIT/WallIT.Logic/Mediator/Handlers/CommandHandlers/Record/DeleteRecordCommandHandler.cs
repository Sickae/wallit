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
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public DeleteRecordCommandHandler(ISession session, IUnitOfWork unitOfWork)
        {
            _session = session;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _unitOfWork.BeginTransaction();
            var record = _session.Load<RecordEntity>(request.Id);
            using (var trans = _session.BeginTransaction())
            {
                _session.Delete(record);
                trans.Commit();
            }
            return new ActionResult { Suceeded = true };
        }
    }
}
