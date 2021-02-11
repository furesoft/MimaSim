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

        public int Index => 1;
        public string Title => "Prozessor";

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}