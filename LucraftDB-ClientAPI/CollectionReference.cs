﻿using Lucraft.Database.Client.Query;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Lucraft.Database.Client
{
    public class CollectionReference
    {
        private string DbID { get; init; }
        public string ID { get; init; }

        public CollectionReference(string dbID, string id)
        {
            DbID = dbID;
            ID = id;
        }

        public CollectionSnapshot Get()
        {
            string req = $"get /{DbID}/{ID}/*";
            string res = DataStorage.Instance.MakeRequest(req);
            return JsonConvert.DeserializeObject<CollectionSnapshot>(res);
        }

        public void Set(object data)
        {
            DataStorage.Instance.MakeRequest($"get /{DbID}/{ID}/* {JsonConvert.SerializeObject(data)}");
        }

        public QuerySnapshot Query(string queryStr)
        {
            string req = $"get /{DbID}/{ID}/* where {queryStr}";
            string res = DataStorage.Instance.MakeRequest(req);
            return JsonConvert.DeserializeObject<QuerySnapshot>(res);
        }

        public QuerySnapshot Query<T>(Expression<Func<T, bool>> queryExpr)
        {
            string exprStr = LambdaUtilities.LambdaToString<T>(queryExpr);
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