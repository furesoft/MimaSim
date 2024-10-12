using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.Popups;

public partial class ErrorPopup : ReactiveUserControl<ErrorPopupViewModel>
{
    public ErrorPopup()
    {
        DataContext = new ErrorPopupViewModel();

        this.WhenActivated(disposables => { /* Handle view activation etc. */ });

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}