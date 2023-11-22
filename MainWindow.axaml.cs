using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using avaloniaClient.StaticData;

namespace avaloniaClient;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        SetSettingsBtn.IsEnabled = false;
        label1.IsVisible = false;
        label2.Foreground = new SolidColorBrush(Colors.Gray);
        label3.Foreground = new SolidColorBrush(Colors.Gray);
        FileGeneralData = "";
        FileInputData = "";
        SkinDropdownBox.IsEnabled = false;
        _fileUtils = new(StorageProvider);
        var endpoint = new StaticChampionEndpoint();
        var list = endpoint.GetAllAsync().Result.Champions.Values.ToArray();
        foreach (var championStatic in list)
        {
            ChampionDropdownBox.Items.Add(championStatic);
        }
    }

    private readonly FileUtils _fileUtils;
    private string? FileInputData { get; set; }
    private string? FileGeneralData { get; set; }
    private void ExpireLabel()
    {
        new Thread(async () =>
        {
            await Task.Delay(2500);
            Dispatcher.UIThread.Invoke(() =>
            {
                label1.Text = "";
            });
        }).Start();
    }
    private bool CheckIfConnected(out HttpConnection? http)
    {
        var leagueInfo = LeagueUtils.GetLeagueStatus();
        if (leagueInfo != null)
        {
            http = new HttpConnection(leagueInfo.Item3, leagueInfo.Item2);
            return true;
        }
        label1.Text = "League client is not connected!";
        label1.Foreground = new SolidColorBrush(Colors.Red);
        label1.IsVisible = true;
        ExpireLabel();
        http = null;
        return false;
    }

    private void ShowSuccessLabel()
    {
        label1.Text = "Success!";
        label1.Foreground = new SolidColorBrush(Colors.Green);
        label1.IsVisible = true;
        ExpireLabel();
    }
    
    private void ShowFailedPickingFileLabel()
    {
        label1.Text = "Failed picking the file.";
        label1.Foreground = new SolidColorBrush(Colors.Red);
        label1.IsVisible = true;
        ExpireLabel();
    }
    
    private async Task<bool> ExecutePatchFromImported(HttpConnection http, string endpoint, string? data)
    {
        if (data != null)
        {
            var resp = await http.Patch(endpoint, data);
            if (!resp.IsSuccessStatusCode)
            {
                label1.Text = $"Incorrect file!(Status code: {resp.StatusCode})";
                label1.Foreground = new SolidColorBrush(Colors.Red);
                label1.IsVisible = true;
                ExpireLabel();
                return false;
            }
            ShowSuccessLabel();
            return true;
        }
        ExpireLabel();
        return false;
    }

    private async Task<bool> ExecutePost(HttpConnection http, string endpoint, string data)
    {
        var resp = await http.Post(endpoint, data);
        if (resp.IsSuccessStatusCode)
        {
            ShowSuccessLabel();
            return true;
        }

        ExpireLabel();
        return false;
    }
    private async Task<bool> ExecutePut(HttpConnection http, string endpoint, string data)
    {
        var resp = await http.Put(endpoint, data);
        if (resp.IsSuccessStatusCode)
        {
            ShowSuccessLabel();
            return true;
        }
        ExpireLabel();
        return false;
    }

    private async Task<string?> ImportSettingsFile(TextBlock label)
    {
        var file = await _fileUtils.PickTextFile();
        if (file == null)
        {
            ShowFailedPickingFileLabel();
            return null;
        }
        SetSettingsBtn.IsEnabled = true;
        label.Text = file.Name; //.Split("\\", StringSplitOptions.TrimEntries)[^1];
        label.Foreground = new SolidColorBrush(Colors.White);
        label.IsVisible = true;
        
        return await FileUtils.ReadDataFromFile(file);
    }
    public async void OnImportInputClick(object sender, RoutedEventArgs e)
    {
        FileInputData = await ImportSettingsFile(label2);
    }
    private async void OnImportGeneralClick(object? sender, RoutedEventArgs e)
    {
        FileGeneralData = await ImportSettingsFile(label3);
    }
    
    private async void OnExportProfilesClick(object? sender, RoutedEventArgs e)
    {
        string currentInputSettings;
        string currentGeneralSettings;
        if (CheckIfConnected(out var http))
        {
            currentInputSettings = await http!.Get("/lol-game-settings/v1/input-settings/");
            currentGeneralSettings = await http.Get("/lol-game-settings/v1/game-settings/");
        }
        else return;
        
        var file1 = await _fileUtils.PickSaveFile("LeagueSettings-input.json");
        var file2 = await _fileUtils.PickSaveFile("LeagueSettings-general.json");
        if(file1 == null || file2 == null)
        {
            ShowFailedPickingFileLabel();
            return;
        }
        var success1 = await FileUtils.WriteToFile(file1, currentInputSettings);
        var success2 = await FileUtils.WriteToFile(file2, currentGeneralSettings);
        if(success1 && success2)
            ShowSuccessLabel();
    }
    
    private List<SkinStatic>? _skinList;
    private void OnChampionDropdownChange(object? sender, SelectionChangedEventArgs e)
    {
        var item = ChampionDropdownBox.SelectedItem;
        if (item == null)
            return;
        _skinList = ((ChampionStatic)item).Skins;
        SkinDropdownBox.Items.Clear();
        foreach (var skin in _skinList)
        {
            SkinDropdownBox.Items.Add(skin);
        }
        SkinDropdownBox.SelectedIndex = 0;
        SkinDropdownBox.IsEnabled = true;
        SetSettingsBtn.IsEnabled = true;
    }

    private async void SetChallenges_Click(object? sender, RoutedEventArgs e)
    {
        if (!CheckIfConnected(out var http))
            return;
        if (string.IsNullOrEmpty(challengeSet.Text)) return;
        
        var chs = $"[{challengeSet.Text}]";
        await ExecutePost(http!, "/lol-challenges/v1/update-player-preferences/", "{ \"challengeIds\": " + chs + "}");
    }

    private async void OnSetStatusClick(object? sender, RoutedEventArgs e)
    {
        //var customenter = new string(' ', 89) + " ";
        if (CheckIfConnected(out var http))
            await ExecutePut(http!, "/lol-chat/v1/me", $"{{\"statusMessage\": \"{StatusMessage.Text}\"}}");
    }

    private async void OnResetTokensClick(object? sender, RoutedEventArgs e)
    {
        if (!CheckIfConnected(out var http))
            return;
        await ExecutePost(http!, "/lol-challenges/v1/update-player-preferences/", "{ \"challengeIds\": [] }");
    }

    private async void OnSetClientSettingsClick(object? sender, RoutedEventArgs e)
    {
        if (!CheckIfConnected(out var http))
            return;
        await ExecutePatchFromImported(http!, "/lol-game-settings/v1/input-settings/", FileInputData);
        await ExecutePatchFromImported(http!, "/lol-game-settings/v1/game-settings/", FileGeneralData);
        FileGeneralData = null;
        label3.Text = "No File";
        label3.Foreground = new SolidColorBrush(Colors.Gray);
        FileInputData = null;
        label2.Text = "No File";
        label2.Foreground = new SolidColorBrush(Colors.Gray);

        if (_skinList != null)
            await ExecutePost(http!, "/lol-summoner/v1/current-summoner/summoner-profile", $"{{\"key\": \"backgroundSkinId\", \"value\": {_skinList[SkinDropdownBox.SelectedIndex].Id}}}");
    }
}