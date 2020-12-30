using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;

namespace MimaSim.Tabs
{
    public class ExecutionTab : UserControl, IUITab
    {
        public ExecutionTab()
        {
            this.InitializeComponent();
        }

        public string Title => "Ausführung";

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}