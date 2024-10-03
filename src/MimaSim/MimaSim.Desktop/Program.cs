using System;
using Avalonia;
using Avalonia.ReactiveUI;
using MimaSim.Core;
using MimaSim.Desktop.Core;
using Splat;

namespace MimaSim.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Locator.CurrentMutable.RegisterLazySingleton<ICache>(() => new DesktopCache());

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseReactiveUI()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}