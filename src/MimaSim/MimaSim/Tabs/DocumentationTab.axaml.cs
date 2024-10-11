using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;
using MimaSim.ViewModels;

namespace MimaSim.Tabs;

public partial class DocumentationTab : UserControl
{
    public DocumentationTab()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}