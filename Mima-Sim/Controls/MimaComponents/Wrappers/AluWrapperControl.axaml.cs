using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.Wrappers
{
    public class AluWrapperControl : UserControl
    {
        public AluWrapperControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
