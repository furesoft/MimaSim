using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;
using MimaSim.ViewModels;

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

        public object ViewModel => new TablesViewModel();

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}