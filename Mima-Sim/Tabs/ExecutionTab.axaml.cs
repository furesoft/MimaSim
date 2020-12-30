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

        public string Title => "Prozessor";

        public int Index => 1;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}