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

        public string Title => "Doku";

        public int Index => 2;

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}