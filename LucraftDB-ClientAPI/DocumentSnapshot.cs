using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lucraft.Database.Client
{
    public class DocumentSnapshot
    {
        [JsonProperty("id")]
        public string ID { get; init; }
        [JsonProperty("exists")]
        public bool Exists { get; init; }
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; init; }

        public T ConvertTo<T>()
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(Data));
        }
    }
}