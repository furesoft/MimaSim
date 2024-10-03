using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.Core;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Tabs;

public partial class ExecutionTab : ReactiveUserControl<ExecutionTabViewModel>, IUITab
{
    public ExecutionTab()
    {
        DataContext = new ExecutionTabViewModel();

        this.WhenActivated(disposables => { /* Handle view activation etc. */ });

        InitializeComponent();
    }

    public int Index => 1;
    public string Title => "Prozessor";

    object IUITab.ViewModel => new ExecutionTabViewModel();

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}