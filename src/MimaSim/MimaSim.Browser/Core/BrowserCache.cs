using System.Runtime.InteropServices.JavaScript;
using MimaSim.Core;
using Newtonsoft.Json;

namespace MimaSim.Browser.Core;

public partial class Interop : IInterop
{
    [JSImport("globalThis.window.open")]
    private static partial void OpenWindow(string url, string target);

    public void OpenLink(string url)
    {
        OpenWindow(url, "_blank");
    }
}

public partial class BrowserCache : ICache
{
    [JSImport("globalThis.localStorage.setItem")]
    private static partial void SetItem(string key, string value);

    [JSImport("globalThis.localStorage.getItem")]
    private static partial string GetItem(string key);

    public void AddOrUpdate(string key, object value)
    {
        SetItem(key, JsonConvert.SerializeObject(value));
    }

    public T? Get<T>(string key)
    {
        var jsonValue = GetItem(key);

        return string.IsNullOrEmpty(jsonValue) ? default : JsonConvert.DeserializeObject<T>(jsonValue);
    }
}