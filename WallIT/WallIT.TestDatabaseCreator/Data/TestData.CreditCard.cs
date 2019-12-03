using WallIT.DataAccess.Entities;

namespace WallIT.TestDatabaseCreator.Data
{
    static partial class TestData
    {
        internal static void CreateCreditCards()
        {
            for (var i = 0; i < 3; i++)
            {
                var cc = new CreditCardEntity
                {
                    Name = $"Credit Card #{i + 1}"
                };

                InsertEntity(cc);
            }
        }
    }
}
