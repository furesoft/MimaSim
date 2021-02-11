using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MimaSim.MIMA.Components;
using ReactiveUI;
using Splat;
using System.Reflection;

namespace MimaSim
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            CPU.Instance.Init();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

            base.OnFrameworkInitializationCompleted();
        }
    }
}