using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lucraft.Database.Client
{
    public class CollectionSnapshot
    {
        [JsonProperty("id")]
        public string Id { get; init; }
        [JsonProperty("Documents")]
        public List<DocumentSnapshot> Documents { get; init; }
    }
}