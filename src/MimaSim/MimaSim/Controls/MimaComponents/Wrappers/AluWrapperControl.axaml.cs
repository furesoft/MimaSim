using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.MimaComponents.Wrappers;

public partial class AluWrapperControl : UserControl
{
    public AluWrapperControl()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}