using System.Collections.Generic;
using MimaSim.Core;
using System.IO;
using Newtonsoft.Json;

namespace MimaSim.Desktop.Core;

public class DesktopCache : ICache
{
    private const string CacheFilePath = "./cache.json";

    public DesktopCache()
    {
        if (!File.Exists(CacheFilePath))
        {
            File.WriteAllText(CacheFilePath, "{}"); // Initialize with an empty JSON object
        }
    }

    public void AddOrUpdate(string key, object value)
    {
        var cacheData = LoadCache();
        cacheData[key] = JsonConvert.SerializeObject(value);
        SaveCache(cacheData);
    }

    public T Get<T>(string key)
    {
        var cacheData = LoadCache();

        if (cacheData.TryGetValue(key, out var jsonValue))
        {
            return JsonConvert.DeserializeObject<T>(jsonValue);
        }

        return default; // Return default value for type T if not found
    }

    private Dictionary<string, string> LoadCache()
    {
        var json = File.ReadAllText(CacheFilePath);

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
    }

    private void SaveCache(Dictionary<string, string> cacheData)
    {
        var json = JsonConvert.SerializeObject(cacheData, Formatting.Indented);

        File.WriteAllText(CacheFilePath, json);
    }
}