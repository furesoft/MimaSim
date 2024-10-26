using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.Popups;

public partial class HelpPopup : UserControl
{
    public HelpPopup()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}