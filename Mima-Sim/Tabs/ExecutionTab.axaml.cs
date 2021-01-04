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

            this.Initialized += ExecutionTab_Initialized;
        }

        private void ExecutionTab_Initialized(object sender, System.EventArgs e)
        {
            BusRegistry.GetBusMap("control->adr").Activate();
        }

        public string Title => "Prozessor";

        public int Index => 1;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}