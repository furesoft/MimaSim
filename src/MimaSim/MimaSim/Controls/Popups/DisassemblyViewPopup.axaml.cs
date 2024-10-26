using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using AvaloniaHex;
using AvaloniaHex.Rendering;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.Popups;

public partial class DisassemblyViewPopup : ReactiveUserControl<DisassemblyPopupViewModel>
{
    public DisassemblyViewPopup()
    {
        DataContext = new DisassemblyPopupViewModel();
        var cc = DataContext as IActivatableViewModel;
        cc!.Activator.Activate();

        InitializeComponent();

        InitHexView();
    }

    private void InitHexView()
    {
        MainHexEditor = this.Find<HexEditor>("MainHexEditor")!;

        MainHexEditor.HexView.BytesPerLine = 12;

        var layer = MainHexEditor.HexView.Layers.Get<CellGroupsLayer>();
        layer.BytesPerGroup = 4;
        layer.Backgrounds.Add(new SolidColorBrush(Colors.Gray, 0.1D));
        layer.Backgrounds.Add(null);
        layer.Border = new Pen(Brushes.Gray, dashStyle: DashStyle.Dash);

        MainHexEditor.HexView.InvalidateVisualLines();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}