using MediatR;
using NHibernate;
using WallIT.DataAccess.Entities;
using System.Threading;
using System.Threading.Tasks;
using WallIT.Logic.DTOs;
using WallIT.Logic.Mediator.Commands;
using WallIT.Shared.Interfaces.UnitOfWork;

namespace WallIT.Logic.Mediator.Handlers.CommandHandlers
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public DeleteAccountCommandHandler(ISession session, IUnitOfWork unitOfWork)
        {
            _session = session;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _unitOfWork.BeginTransaction();
            var account = _session.Load<AccountEntity>(request.Id);
            using (var trans = _session.BeginTransaction())
            {
                _session.Delete(account);
                trans.Commit();
            }
            return new ActionResult { Suceeded = true };
        }
    }
}
