using Lucraft.Database.Client.Query;
using System;

namespace Lucraft.Database.Client.Test
{
    class Program
    {
        static void Main()
        {
            DataStorage.SetInstance("localhost");

            CollectionReference collection = DataStorage.Instance.GetDatabase("website").GetCollection("accounts");

            string email = "example@example.com";
            QuerySnapshot querySnapshot = collection.Query<Account>((account) => account.Email == email);

            querySnapshot.Documents.ForEach((doc) =>
            {
                Console.WriteLine(doc.ID + " -> " + doc.ConvertTo<Account>().ToString());
            });
        }
    }
}
