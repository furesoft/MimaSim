using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.Core;
using ReactiveUI;

namespace MimaSim
{
    public class MainWindow : ReactiveWindow<object>
    {
        public MainWindow()
        {
            this.WhenActivated(disposables =>
            {
                var tabControl = this.FindControl<TabControl>("content");

                TabSwitcher.Initialize(tabControl);
            });

            AvaloniaXamlLoader.Load(this);

#if DEBUG
            this.AttachDevTools();
#endif
        }
    }
}