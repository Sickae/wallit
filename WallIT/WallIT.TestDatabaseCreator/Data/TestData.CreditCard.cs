using NHibernate;
using System;
using WallIT.DataAccess.Entities;

namespace WallIT.TestDataBaseCreator.Data
{
    static partial class TestData
    {
        internal static void CreateCreditCards(ISession session)
        {
            Console.WriteLine("Inserting Credit Cards...");
            for (var i = 0; i < 3; i++)
            {
                var cc = new CreditCardEntity
                {
                    Name = $"Credit Card #{i + 1}"
                };

                session.Save(cc);
            }
        }
    }
}
