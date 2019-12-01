﻿using System;

namespace WallIT.Shared.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        bool IsManagedTransaction { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();

        void InTransaction(Action action);
    }
}
