using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.MimaComponents
{
    public class MemoryControl : UserControl
    {
        public MemoryControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}