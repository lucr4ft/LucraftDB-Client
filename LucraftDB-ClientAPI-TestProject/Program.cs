using Lucraft.Database.Client.Query;
using System;
using System.Threading.Tasks;

namespace Lucraft.Database.Client.Test
{
    class Program
    {
        static async Task Main()
        {
            DataStorage.SetInstance("localhost");

            await DataStorage.ConnectAsync();

            CollectionReference collection = DataStorage.GetInstance().GetDatabase("website").GetCollection("accounts");

            string email = "example@example.com";
            QuerySnapshot querySnapshot = collection.Query<Account>((account) => account.Email == email);

            querySnapshot.Documents.ForEach((doc) =>
            {
                Console.WriteLine(doc.Id + " -> " + doc.ConvertTo<Account>().ToString());
            });
        }
    }
}
