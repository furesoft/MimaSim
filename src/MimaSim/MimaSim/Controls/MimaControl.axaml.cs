using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls;

public partial class MimaControl : UserControl
{
    public MimaControl()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}