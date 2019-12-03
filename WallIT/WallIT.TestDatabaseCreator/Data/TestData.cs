using NHibernate;
using System;
using WallIT.Shared.Interfaces.DomainModel.Entity;

namespace WallIT.TestDatabaseCreator.Data
{
    static partial class TestData
    {
        internal static ISession session;

        internal static void InsertEntity(IEntity entity)
        {
            if (session == null)
                throw new InvalidOperationException("Session must be provided!");

            session.Save(entity);
            Console.WriteLine($"INSERTED {entity.ToString()} ({entity.Id})");
        }
    }
}
