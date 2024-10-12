using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.Popups;

public partial class DisassemblyViewPopup : ReactiveUserControl<DisassemblyPopupViewModel>
{
    public DisassemblyViewPopup()
    {
        DataContext = new DisassemblyPopupViewModel();
        var cc = DataContext as IActivatableViewModel;
        cc!.Activator.Activate();

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}