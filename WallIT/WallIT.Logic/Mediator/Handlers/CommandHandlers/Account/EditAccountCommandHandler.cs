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
    public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand, ActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        internal static ISession _session;
        public EditAccountCommandHandler(ISession session, IUnitOfWork unitOfWork)
        {
            _session = session;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> Handle(EditAccountCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _unitOfWork.BeginTransaction();
            var user = _session.Load<UserEntity>(request.account.UserId);
            using (var trans = _session.BeginTransaction())
            {
                var account = new AccountEntity
                {
                    Balance = request.account.Balance,
                    AccountType = request.account.AccountType,
                    Currency = request.account.Currency,
                    Name = request.account.Name,
                    ModificationDateUTC = DateTime.UtcNow,
                    User = user
                };
                _session.Update(account);
                trans.Commit();
            }

            return new ActionResult { Suceeded = true };
        }
    }
}
