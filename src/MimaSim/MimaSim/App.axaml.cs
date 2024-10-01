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

        CPU.Instance.Init();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                //DataContext = new MainViewModel()
            };
            Locator.CurrentMutable.Register<IStorageProvider>(() => desktop.MainWindow.StorageProvider);

        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                //DataContext = new MainViewModel()
            };
            Locator.CurrentMutable.Register<IStorageProvider>(() => TopLevel.GetTopLevel(singleViewPlatform.MainView)!.StorageProvider);
        }

        var samples = new SampleLoader();
        samples.Register(LanguageName.Maschinencode, "Addition", "04 01 00 40 01 02 04 02 00 40 01 03 08");

        Locator.CurrentMutable.RegisterConstant(samples);

        base.OnFrameworkInitializationCompleted();
    }
}