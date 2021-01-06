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
            //DataStorage.SetInstance("lucraft.ddns.net");

            //QuerySnapshot<Account> querySnapshot = await DataStorage.Instance.GetDatabase("test").GetCollection("test3").Query<Account>(
            //    (acc) => acc.Email != "luca.lewin23@gmail.com" || acc.Username != "Luca Lewin" || acc.PasswordHash == acc.Username);
            //string query = ToQueryString<Account>(
            //    (acc) => acc.Email != "luca.lewin23@gmail.com" ||
            //             acc.Username != "Luca Lewin" ||
            //             acc.PasswordHash == acc.Username);
            //ConditionParser parser = new ConditionParser();

            //var watch = System.Diagnostics.Stopwatch.StartNew();

            //for (int i = 0; i < 1000; i++) {
            //    parser.GetCondition(query);
            //}

            //watch.Stop();
            //Console.WriteLine(watch.ElapsedTicks);

            DataStorage.SetInstance("192.168.178.60");
            DatabaseReference db = DataStorage.Instance.GetDatabase("website");

            string test = "luca.lewin1203@gmail.com";
            //Console.WriteLine(ToQueryString<Account>((account) => account.Email == "{0}", test));



            QuerySnapshot querySnapshot = db.GetCollection("accounts").Query<Account>((account) => account.Email == "{0}", test);
            //querySnapshot.Documents.ForEach(doc =>
            //{
            //    if (doc.Exists)
            //    {
            //        Console.WriteLine("Found document with id: " + doc.ID + " | " + doc.ConvertTo<Account>());
            //    }
            //});
        }


        private static string ToQueryString<T>(Expression<Func<T, bool>> queryExpr, params object[] args)
        {
            Console.WriteLine(queryExpr.Body.ReduceExtensions().ToString());

            string exprBody = queryExpr.Body.ToString();
            string paramName = queryExpr.Parameters[0].Name;

            foreach (var property in typeof(T).GetProperties())
            {
                var attributes = (DatabaseProperty[])property.GetCustomAttributes(typeof(DatabaseProperty), false);
                foreach (var attribute in attributes)
                {
                    exprBody = exprBody.Replace(paramName + "." + property.Name, attribute.name);
                }
            }
            string queryStr = exprBody.Replace(paramName + ".", "").Replace("AndAlso", "&&").Replace("OrElse", "||");

            //string requestStr = $"get /{dbID}/{ID}/*?{queryStr}";

            Console.WriteLine(queryStr);

            return string.Format(queryStr, args);
        }

    }
}
