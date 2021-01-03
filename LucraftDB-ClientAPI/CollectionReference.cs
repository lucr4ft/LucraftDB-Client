using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lucraft.Database.Client
{
    public class CollectionReference
    {
        private string dbID { get; set; }
        public string ID { get; private set; }

        public CollectionReference(string dbID, string id)
        {
            this.dbID = dbID;
            this.ID = id;
        }

        public async Task<CollectionSnapshot> Get()
        {
            return null;
        }

        public async Task<QuerySnapshot> Query(string queryStr)
        {
            return null;
        }

        public async Task<QuerySnapshot> Query<T>(Expression<Func<T, bool>> queryExpr)
        {
            Console.WriteLine(queryExpr.Body.ToString());

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
            string requestStr = $"get /{dbID}/{ID}/* where {queryStr}";

            Console.WriteLine(requestStr);

            return null;
        }
    }
}