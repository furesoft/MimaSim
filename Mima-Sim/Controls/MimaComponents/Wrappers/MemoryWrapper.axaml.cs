using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.Wrappers
{
    public class MemoryWrapper : UserControl
    {
        public MemoryWrapper()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
