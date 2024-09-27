using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.MimaComponents.Popups;

public partial class ErrorPopupControl : ReactiveUserControl<ErrorPopupViewModel>
{
    public ErrorPopupControl()
    {
        DataContext = new ErrorPopupViewModel();

        this.WhenActivated(disposables => { /* Handle view activation etc. */ });

        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}