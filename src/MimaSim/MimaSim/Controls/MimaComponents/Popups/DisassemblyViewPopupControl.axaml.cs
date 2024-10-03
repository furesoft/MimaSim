using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.MimaComponents.Popups;

public partial class DisassemblyViewPopupControl : ReactiveUserControl<DisassemblyPopupViewModel>
{
    public DisassemblyViewPopupControl()
    {
        DataContext = new DisassemblyPopupViewModel();
        var cc = DataContext as IActivatableViewModel;
        cc.Activator.Activate();

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}