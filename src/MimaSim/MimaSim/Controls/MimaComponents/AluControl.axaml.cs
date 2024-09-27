using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.MimaComponents;

public partial class AluControl : UserControl
{
    public AluControl()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}