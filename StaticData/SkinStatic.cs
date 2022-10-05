using Newtonsoft.Json;

namespace ClientSettingsLoader.StaticData
{
    /// <summary>
    /// Class representing a skin of a champion (Static API).
    /// </summary>
    public class SkinStatic
    {
        internal SkinStatic() { }

        /// <summary>
        /// Id of the skin.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the skin.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Ordered number of the skin.
        /// </summary>
        [JsonProperty("num")]
        public int Num { get; set; }

        public override string ToString() => Name;
    }
}
