using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lucraft.Database.Client
{
    public class DocumentReference
    {
        public string Id { get; }
        private string CollectionId { get; }
        private string DatabaseId { get; }

        internal DocumentReference(string dbId, string collId, string id)
        {
            DatabaseId = dbId;
            CollectionId = collId;
            Id = id;
        }

        public DocumentSnapshot Get()
        {
            string req = $"{RequestType.Get} /{DatabaseId}/{CollectionId}/{Id}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<DocumentSnapshot>(res);
        }

        public WriteResult Set(IDictionary<string, object> data)
        {
            string req = $"{RequestType.Set} /{DatabaseId}/{CollectionId}/{Id} {JsonConvert.SerializeObject(data)}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<WriteResult>(res);
        }

        public WriteResult Delete()
        {
            string req = $"{RequestType.Delete} /{DatabaseId}/{CollectionId}/{Id}";
            string res = DataStorage.MakeRequest(req);
            return JsonConvert.DeserializeObject<WriteResult>(res);
        }
    }
}
