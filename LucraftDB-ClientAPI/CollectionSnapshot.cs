using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lucraft.Database.Client
{
    public class CollectionSnapshot
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("Documents")]
        public List<DocumentSnapshot> Documents { get; set; }
    }
}