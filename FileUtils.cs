using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace avaloniaClient;

public class FileUtils
{
    private IStorageProvider StorageProvider;
    private string? _preferredPath;
    
    public FileUtils(IStorageProvider storageProvider)
    {
        StorageProvider = storageProvider;
    }
    public static async Task<bool> WriteToFile(IStorageFile? file, string data)
    {
        if (file == null) return false;
        await using var stream = await file.OpenWriteAsync();
        await using var streamWriter = new StreamWriter(stream);
        await streamWriter.WriteLineAsync(data);
        return true;
    }
    
    public static async Task<string?> ReadDataFromFile(IStorageFile? file)
    {
        if (file == null) return null;
        await using var stream = await file.OpenReadAsync();
        using var sr = new StreamReader(stream);
        return await sr.ReadToEndAsync();
    }
    
    
    private async Task<IStorageFolder?> GetPreferredPickerFolder()
    {
        IStorageFolder? folder;
        if (_preferredPath != null)
            folder = await StorageProvider.TryGetFolderFromPathAsync(new Uri(_preferredPath));
        else folder = await StorageProvider.TryGetWellKnownFolderAsync(WellKnownFolder.Desktop);
        return folder;
    }
    
    public async Task<IStorageFile?> PickTextFile()
    {
        var file = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            AllowMultiple = false,
            SuggestedStartLocation = await GetPreferredPickerFolder(),
            FileTypeFilter = new[]{FilePickerFileTypes.TextPlain}
        });
        if (!file.Any()) return null;
        _preferredPath = Path.GetDirectoryName(file[0].Path.AbsolutePath);
        return file[0];
    }

    public async Task<IStorageFile?> PickSaveFile(string fileName)
    {
        var file = await StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            SuggestedStartLocation = await GetPreferredPickerFolder(),
            SuggestedFileName = fileName
        });
        if(file != null)
            _preferredPath = Path.GetDirectoryName(file.Path.AbsolutePath);
        return file;
    }
}