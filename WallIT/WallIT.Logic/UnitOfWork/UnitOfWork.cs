using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using WallIT.Shared.Interfaces.UnitOfWork;

namespace WallIT.Logic.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;

        private ITransaction transaction;

        public UnitOfWork(ISession session)
        {
            _session = session;
            transaction = null;
        }

        public void BeginTransaction()
        {
            if (transaction != null && transaction.IsActive)
                throw new InvalidOperationException("There is an open transaction already!");

            transaction = _session.BeginTransaction();
        }

        public void Commit()
        {
            // TODO localization
            if (transaction == null || !transaction.IsActive)
                throw new InvalidOperationException("There is no transaction to commit!");

            transaction.Commit();
        }

        public void InTransaction(Action action)
        {
            if (transaction != null)
                throw new InvalidOperationException("There is an open transaction already!");

            using (var trans = _session.BeginTransaction())
            {
                action();
                trans.Commit();
            }
        }

        public void Rollback()
        {
            // TODO localization
            if (transaction == null || !transaction.IsActive)
                throw new InvalidOperationException("There is no transaction to roll back!");

            transaction.Rollback();
            transaction.Dispose();
            transaction = null;

            _session.Clear();
        }
    }
}
