using Avalonia;
using Avalonia.ReactiveUI;

namespace MimaSim
{
    internal class Program
    {
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UseReactiveUI()
                .UsePlatformDetect()
                .LogToTrace();

        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }
}