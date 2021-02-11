using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.OtherPopups
{
    public class LicensesPopupControl : UserControl
    {
        public LicensesPopupControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
