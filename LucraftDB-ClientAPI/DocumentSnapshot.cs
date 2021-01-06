using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lucraft.Database.Client
{
    public class DocumentSnapshot
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("exists")]
        public bool Exists { get; set; }
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }

        public T ConvertTo<T>()
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(Data));
        }
    }
}