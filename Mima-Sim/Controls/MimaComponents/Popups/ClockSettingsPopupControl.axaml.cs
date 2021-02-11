using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;

namespace MimaSim.Controls.MimaComponents.Popups
{
    public class ClockSettingsPopupControl : ReactiveUserControl<ClockSettingsPopupViewModel>
    {
        public ClockSettingsPopupControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}