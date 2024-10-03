using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.Samples;
using MimaSim.Views;
using ReactiveUI;
using Splat;

namespace MimaSim;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                //DataContext = new MainViewModel()
            };
            Locator.CurrentMutable.Register(() => desktop.MainWindow.StorageProvider);

        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                //DataContext = new MainViewModel()
            };
            Locator.CurrentMutable.Register(() => TopLevel.GetTopLevel(singleViewPlatform.MainView)!.StorageProvider);
        }

        var samples = new SampleLoader();
        samples.FromResources(GetType().Assembly);

        Locator.CurrentMutable.RegisterConstant(samples);

        base.OnFrameworkInitializationCompleted();
    }
}