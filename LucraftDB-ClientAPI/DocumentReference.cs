using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lucraft.Database.Client
{
    public class DocumentReference
    {
        public string ID { get; init; }
        private string CollectionID { get; init; }
        private string DatabaseID { get; init; }

        internal DocumentReference(string dbID, string colID, string id)
        {
            DatabaseID = dbID;
            CollectionID = colID;
            ID = id;
        }

        public DocumentSnapshot Get()
        {
            string req = $"{RequestType.Get} /{DatabaseID}/{CollectionID}/{ID}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<DocumentSnapshot>(res);
        }

        public WriteResult Set(IDictionary<string, object> data)
        {
            string req = $"{RequestType.Set} /{DatabaseID}/{CollectionID}/{ID} {JsonConvert.SerializeObject(data)}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<WriteResult>(res);
        }

        public WriteResult Delete()
        {
            string req = $"{RequestType.Delete} /{DatabaseID}/{CollectionID}/{ID}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<WriteResult>(res);
        }
    }
}
