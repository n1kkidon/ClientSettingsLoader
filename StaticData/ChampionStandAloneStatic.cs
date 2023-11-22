using System.Collections.Generic;
using Newtonsoft.Json;

namespace avaloniaClient.StaticData
{
    internal class ChampionStandAloneStatic
    {
        [JsonProperty("type")]
        internal string Type { get; set; }

        [JsonProperty("format")]
        internal string Format { get; set; }

        [JsonProperty("Version")]
        internal string Version { get; set; }
        
        [JsonProperty("data")]
        internal Dictionary<string, ChampionStatic> Data { get; set; }
    }
}
