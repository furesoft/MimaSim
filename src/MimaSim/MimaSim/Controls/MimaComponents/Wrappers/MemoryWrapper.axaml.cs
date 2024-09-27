using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.MimaComponents.Wrappers;

public partial class MemoryWrapper : ReactiveUserControl<ExecutionTabViewModel>
{
    public MemoryWrapper()
    {
        this.WhenActivated(disposables =>
        {
            /* Handle view activation etc. */
        });

        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}