using Lucraft.Database.Client.Query;
using Lucraft.Utilities.Reflection;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Lucraft.Database.Client
{
    public class CollectionReference
    {
        private string DbID { get; init; }
        public string ID { get; init; }

        internal CollectionReference(string dbID, string id)
        {
            DbID = dbID;
            ID = id;
        }

        public DocumentReference GetDocument(string id)
        {
            return new DocumentReference(DbID, ID, id);
        }

        public CollectionSnapshot Get()
        {
            string req = $"{RequestType.Get} /{DbID}/{ID}/*";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<CollectionSnapshot>(res);
        }

        public WriteResult Set(object data)
        {
            string req = $"{RequestType.Set} /{DbID}/{ID}/* {JsonConvert.SerializeObject(data)}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<WriteResult>(res);
        }

        public QuerySnapshot Query(string queryStr)
        {
            string req = $"{RequestType.Get} /{DbID}/{ID}/* where {queryStr}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<QuerySnapshot>(res);
        }

        public QuerySnapshot Query<T>(Expression<Func<T, bool>> queryExpr)
        {
            string exprStr = Lambda.LambdaToString(queryExpr);
            string paramName = queryExpr.Parameters[0].Name;
            foreach (var property in typeof(T).GetProperties())
            {
                var attributes = (DatabaseProperty[])property.GetCustomAttributes(typeof(DatabaseProperty), false);
                foreach (var attribute in attributes)
                    exprStr = exprStr.Replace(paramName + "." + property.Name, attribute.name);
            }
            return Query(exprStr);
        }
    }
}