using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MimaSim.Core;
using MimaSim.Views;
using ReactiveUI;
using Splat;

namespace MimaSim;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var samples = new SampleLoader();
        samples.FromResources(GetType().Assembly);

        Locator.CurrentMutable.RegisterConstant(samples);

        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = new MainWindow();
                Locator.CurrentMutable.Register(() => desktop.MainWindow.StorageProvider);
                Locator.CurrentMutable.Register(() => desktop.MainWindow.Launcher);
                break;

            case ISingleViewApplicationLifetime singleViewPlatform:
                singleViewPlatform.MainView = new MainView();
                Locator.CurrentMutable.Register(() => TopLevel.GetTopLevel(singleViewPlatform.MainView)!.StorageProvider);
                Locator.CurrentMutable.Register(() => TopLevel.GetTopLevel(singleViewPlatform.MainView)!.Launcher);
                break;
        }

        base.OnFrameworkInitializationCompleted();
    }
}