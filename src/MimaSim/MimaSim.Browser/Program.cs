﻿using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using Microsoft.JSInterop;
using MimaSim;
using MimaSim.Browser.Core;
using MimaSim.Core;
using Splat;

[assembly: SupportedOSPlatform("browser")]

internal sealed partial class Program
{
    private static Task Main(string[] args)
    {
        Locator.CurrentMutable.RegisterLazySingleton<ICache>(() => new BrowserCache());

        return BuildAvaloniaApp()
            .WithInterFont()
            .UseReactiveUI()
            .StartBrowserAppAsync("out");
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}