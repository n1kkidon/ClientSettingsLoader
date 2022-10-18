using ChallengesAreEvil;
using ClientSettingsLoader.StaticData;

namespace ClientSettingsLoader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Run_Btn.Enabled = false;
            label1.Visible = false;
            label2.ForeColor = Color.Gray;
            label3.ForeColor = Color.Gray;
            FileGeneralData = "";
            FileInputData = "";
            comboBox1.Enabled = false;
            var endpoint = new StaticChampionEndpoint();
            var list = endpoint.GetAllAsync().Result.Champions.Values.ToArray();
            comboBox2.Items.AddRange(list);
        }

        private string? FileInputData { get; set; }
        private string? FileGeneralData { get; set; }
        private LeagueConnection Lc { get; set; }
        private void ExpireLabel()
        {
            new Thread(async () => {
                await Task.Delay(2500);
                label1.Text = "";
            }).Start();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JSON files (.json)|*.json";
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                FileInputData = File.ReadAllText(openFileDialog1.FileName);
                Run_Btn.Enabled = true;
                label2.Text = openFileDialog1.FileName.Split("\\", StringSplitOptions.TrimEntries)[^1];
                label2.ForeColor = Color.White;
                label2.Visible = true;
            }          
        }

        private async void Run_Btn_Click(object sender, EventArgs e)
        {
            if (!CheckIfConnected())
                return;
            await ExecutePatchFromImported("/lol-game-settings/v1/input-settings/", FileInputData);          
            await ExecutePatchFromImported("/lol-game-settings/v1/game-settings/", FileGeneralData);
            FileGeneralData = null;
            label3.Text = "No File";
            label3.ForeColor = Color.Gray;
            FileInputData = null;
            label2.Text = "No File";
            label2.ForeColor = Color.Gray;


            if (skinList!=null)
                await ExecutePost("/lol-summoner/v1/current-summoner/summoner-profile", $"{{\"key\": \"backgroundSkinId\", \"value\": {skinList[comboBox1.SelectedIndex].Id}}}");
        }

        private async Task<bool> ExecutePatchFromImported(string endpoint, string? data)
        {
            if (data != null)
            {
                var resp = await Lc.Patch(endpoint, data);
                if (!resp.IsSuccessStatusCode)
                {
                    label1.Text = $"Incorrect file!(Status code: {resp.StatusCode})";
                    label1.ForeColor = Color.Red;
                    label1.Visible = true;
                    ExpireLabel();
                    return false;
                }
                label1.Text = "Success!";
                label1.ForeColor = Color.Green;
                label1.Visible = true;
                ExpireLabel();
                return true;
            }
            ExpireLabel();
            return false;
        }

        private async Task<bool> ExecutePost(string endpoint, string data)
        {
            var resp = await Lc.Post(endpoint, data);
            if (resp.IsSuccessStatusCode)
            {
                label1.Text = "Success!";
                label1.ForeColor = Color.Green;
                label1.Visible = true;
                ExpireLabel();
                return true;
            }

            ExpireLabel();
            return false;
        }
        private async Task<bool> ExecutePut(string endpoint, string data)
        {
            var resp = await Lc.Put(endpoint, data);
            if (resp.IsSuccessStatusCode)
            {
                label1.Text = "Success!";
                label1.ForeColor = Color.Green;
                label1.Visible = true;
                ExpireLabel();
                return true;
            }
            ExpireLabel();
            return false;
        }
        private bool CheckIfConnected()
        {
            Lc = new LeagueConnection();
            if (Lc.IsConnected)
                return true;
            label1.Text = "League client is not connected!";
            label1.ForeColor = Color.Red;
            label1.Visible = true;
            ExpireLabel();
            return false;
        }

        private async void Export_file_btn_Click(object sender, EventArgs e)
        {
            string CurrentInputSettings;
            string CurrentGeneralSettings;
            if (CheckIfConnected())
            {
                CurrentInputSettings = await Lc.Get("/lol-game-settings/v1/input-settings/");
                CurrentGeneralSettings = await Lc.Get("/lol-game-settings/v1/game-settings/");
            }
            else return;

            //saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Filter = "JSON files (.json)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName+"-input.json", CurrentInputSettings);
                File.WriteAllText(saveFileDialog1.FileName+"-general.json", CurrentGeneralSettings);
            }
        }

        private void Import_game_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JSON files (.json)|*.json";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileGeneralData = File.ReadAllText(openFileDialog1.FileName);
                Run_Btn.Enabled = true;
                label3.Text = openFileDialog1.FileName.Split("\\", StringSplitOptions.TrimEntries)[^1];
                label3.ForeColor = Color.White;
                label3.Visible = true;
            }
        }

        private async void ResetTokens_Btn_Click(object sender, EventArgs e)
        {
            if (!CheckIfConnected())
                return;
            await ExecutePost("/lol-challenges/v1/update-player-preferences/", "{ \"challengeIds\": [] }");

        }

        public List<SkinStatic> skinList;

        private async void setStatusMsg_Click(object sender, EventArgs e)
        {
            var customenter = new string(' ', 89)+" ";
            if (CheckIfConnected())
                await ExecutePut("/lol-chat/v1/me", $"{{\"statusMessage\": \"{textBox2.Lines.JoinColletion2()}\"}}");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            skinList = ((ChampionStatic)comboBox2.SelectedItem).Skins;
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(skinList.ToArray());
            comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = true;
            Run_Btn.Enabled = true;
        }
    }
}