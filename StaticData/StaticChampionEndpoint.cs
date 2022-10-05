using Newtonsoft.Json;

namespace ClientSettingsLoader.StaticData
{

    public class StaticChampionEndpoint
    {
        private const string versionJson = "https://ddragon.leagueoflegends.com/api/versions.json";
        private static string ChampionByKeyUrl = "";

        public StaticChampionEndpoint()
        {
            ChampionByKeyUrl = "http://ddragon.leagueoflegends.com/cdn/" + GetLatestGameVersion() + "/data/en_US/champion/{0}.json";                  
        }
        public string GetLatestGameVersion()
        {
            try
            {
                HttpClient client = new();
                HttpResponseMessage response = client.GetAsync(versionJson).Result;
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<string>>(jsonResponse);
                if (data == null) return "";
                return data[0];
            }
            catch (Exception ex)
            {

            }
            return "";
            
        }

        public async Task<ChampionStatic> GetByKeyAsync(string key)
        {
            HttpClient client = new();
            var json = await client.GetStringAsync(string.Format(ChampionByKeyUrl, key)).ConfigureAwait(false);
            var championStandAlone = JsonConvert.DeserializeObject<ChampionStandAloneStatic>(json);
            return championStandAlone.Data.First().Value;
        }

    }
}
