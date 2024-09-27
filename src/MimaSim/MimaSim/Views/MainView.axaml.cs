using Avalonia.Controls;
using Avalonia.ReactiveUI;
using MimaSim.Core;
using ReactiveUI;

namespace MimaSim.Views;

public partial class MainView : ReactiveUserControl<object>
{
    public MainView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            var tabControl = this.FindControl<TabControl>("content");

            TabSwitcher.Initialize(tabControl);
        });
    }
}