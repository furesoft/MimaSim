using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;

namespace MimaSim.Controls.MimaComponents.Popups
{
    public class RawViewPopupControl : ReactiveUserControl<RawPopupViewModel>
    {
        public RawViewPopupControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}