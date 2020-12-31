using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls
{
    public class ControlUnitControl : UserControl
    {
        public ControlUnitControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
