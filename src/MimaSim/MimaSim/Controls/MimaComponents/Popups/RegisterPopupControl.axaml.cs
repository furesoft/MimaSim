using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.MimaComponents.Popups;

public partial class RegisterPopupControl : UserControl
{
    public RegisterPopupControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}