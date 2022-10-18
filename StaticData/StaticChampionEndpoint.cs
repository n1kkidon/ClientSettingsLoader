using Newtonsoft.Json;

namespace ClientSettingsLoader.StaticData
{

    public class StaticChampionEndpoint
    {
        private const string versionJson = "https://ddragon.leagueoflegends.com/api/versions.json";
        private const string ddragonLink = "http://ddragon.leagueoflegends.com/cdn/";
        private static string ChampionByKeyUrl = "";
        private static string AllChamps = "";

        public StaticChampionEndpoint()
        {
            ChampionByKeyUrl = ddragonLink + GetLatestGameVersion() + "/data/en_US/champion/{0}.json";
            AllChamps = ddragonLink + GetLatestGameVersion() + "/data/en_US/championFull.json";
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

        public async Task<ChampionListStatic> GetAllAsync()
        {
            HttpClient client = new();
            var json = await client.GetStringAsync(AllChamps).ConfigureAwait(false);
            var champs = JsonConvert.DeserializeObject<ChampionListStatic>(json);
            return champs;
        }
    }
}
