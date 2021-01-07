using Lucraft.Database.Client.Query;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lucraft.Database.Client.Test
{
    class Program
    {
        static void Main()
        {

            //(email == value(Lucraft.Database.Client.Test.Program+<>c__DisplayClass0_0).email)

            string email = "lucraft@lucraft.ddns.net";
            string username = null;
            bool verified = true;
            //string query = ToQueryString<Account>((account) => account.Email == email);
            Console.WriteLine(LambdaTests.LambdaToString<Account>((account) => account.Email == email && account.Verified == verified && account.Username != username));
            
        }
    }
}
