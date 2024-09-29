using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using MimaSim.ViewModels.Mima;
using ReactiveUI;

namespace MimaSim.Controls.MimaComponents.Popups;

public partial class ClockSettingsPopupControl : ReactiveUserControl<ClockSettingsPopupViewModel>
{
    public ClockSettingsPopupControl()
    {
        DataContext = new ClockSettingsPopupViewModel();

        this.WhenActivated(disposables => { /* Handle view activation etc. */ });

        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}