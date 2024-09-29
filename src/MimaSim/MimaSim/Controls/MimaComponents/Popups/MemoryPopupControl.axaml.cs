using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using MimaSim.ViewModels.Mima;
using ReactiveUI;

namespace MimaSim.Controls.MimaComponents.Popups;

public partial class MemoryPopupControl : ReactiveUserControl<MemoryPopupViewModel>
{
    public MemoryPopupControl()
    {
        DataContext = new MemoryPopupViewModel();

        this.WhenActivated(disposables => { /* Handle view activation etc. */ });

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}