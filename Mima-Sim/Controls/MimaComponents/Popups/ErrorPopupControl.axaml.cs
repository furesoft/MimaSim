using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;

namespace MimaSim.Controls.MimaComponents.Popups
{
    public class ErrorPopupControl : ReactiveUserControl<ErrorPopupViewModel>
    {
        public ErrorPopupControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}