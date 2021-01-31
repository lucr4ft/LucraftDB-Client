using Lucraft.Database.Client.Query;
using Lucraft.Utilities.Reflection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lucraft.Database.Client
{
    public class CollectionReference
    {
        private string DbId { get; }
        public string Id { get; }

        internal CollectionReference(string dbId, string id)
        {
            DbId = dbId;
            Id = id;
        }

        public DocumentReference GetDocument(string id)
        {
            return new DocumentReference(DbId, Id, id);
        }

        public CollectionSnapshot Get()
        {
            string req = $"{RequestType.Get} /{DbId}/{Id}/*";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<CollectionSnapshot>(res);
        }

        public WriteResult Set(object data)
        {
            string req = $"{RequestType.Set} /{DbId}/{Id}/* {JsonConvert.SerializeObject(data)}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<WriteResult>(res);
        }

        public QuerySnapshot Query(string queryStr)
        {
            string req = $"{RequestType.Get} /{DbId}/{Id}/* where {queryStr}";
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
                exprStr = attributes.Aggregate(exprStr,
                    (current,
                        attribute) => current.Replace(paramName + "." + property.Name,
                        attribute.Name));
            }
            return Query(exprStr);
        }
    }
}