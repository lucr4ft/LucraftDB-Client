using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Lucraft.Database.Client.Query
{
    public class QuerySnapshot
    {
        [JsonProperty("documents")]
        public List<DocumentSnapshot> Documents { get; init; }
    }
}