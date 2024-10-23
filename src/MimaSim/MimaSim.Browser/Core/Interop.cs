using System.Runtime.InteropServices.JavaScript;
using MimaSim.Core;

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