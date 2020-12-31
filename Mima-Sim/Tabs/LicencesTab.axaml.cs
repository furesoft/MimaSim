using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;

namespace MimaSim.Tabs
{
    public class LicencesTab : UserControl, IUITab
    {
        public LicencesTab()
        {
            this.InitializeComponent();
        }

        public string Title => "Lizenzen";

        public int Index => 3;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}