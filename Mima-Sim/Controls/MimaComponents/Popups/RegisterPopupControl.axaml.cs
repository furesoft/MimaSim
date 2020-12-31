using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.Popups
{
    public class RegisterPopupControl : UserControl
    {
        public RegisterPopupControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
