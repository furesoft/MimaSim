using Avalonia;
using Avalonia.Dialogs;
using Avalonia.ReactiveUI;

namespace MimaSim
{
    internal class Program
    {
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UseReactiveUI()
                //.UseManagedSystemDialogs()
                .UsePlatformDetect()
                .LogToTrace();

        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }
}