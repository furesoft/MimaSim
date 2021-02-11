using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;

namespace MimaSim.Tabs
{
    public class DocumentationTab : UserControl, IUITab
    {
        public DocumentationTab()
        {
            this.InitializeComponent();
        }

        public int Index => 2;
        public string Title => "Dokumentation";

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}