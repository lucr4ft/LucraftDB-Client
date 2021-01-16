using Lucraft.Database.Client.Query;
using System;

namespace Lucraft.Database.Client.Test
{
    class Program
    {
        static void Main()
        {

            //(email == value(Lucraft.Database.Client.Test.Program+<>c__DisplayClass0_0).email)

            //string email = "lucraft@lucraft.ddns.net";
            //string username = null;
            //bool verified = true;
            //string query = ToQueryString<Account>((account) => account.Email == email);
            //Console.WriteLine(LambdaTests.LambdaToString<Account>((account) => account.Email == email && account.Verified == verified && account.Username == username));

            DataStorage.SetInstance("localhost");

            CollectionReference collection = DataStorage.Instance.GetDatabase("website").GetCollection("accounts");

            string email = "luca.lewin1203@gmail.com";
            QuerySnapshot querySnapshot = collection.Query<Account>((account) => account.Email == email);

            querySnapshot.Documents.ForEach((doc) =>
            {
                Console.WriteLine(doc.ID + " -> " + doc.ConvertTo<Account>().ToString());
            });

            DocumentReference reference = collection.GetDocument("st8dlZE25EqjVNy0rXacNg");
            DocumentSnapshot snapshot = reference.Get();

            Console.WriteLine(snapshot.ConvertTo<Account>().ToString());

        }
    }
}
