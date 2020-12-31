using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.Wrappers
{
    public class ControlUnitWrapper : UserControl
    {
        public ControlUnitWrapper()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
