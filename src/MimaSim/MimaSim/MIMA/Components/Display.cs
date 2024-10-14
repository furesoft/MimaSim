using System;
using Avalonia.Media;
using Avalonia.Threading;
using MimaSim.Controls.MimaComponents;

namespace MimaSim.MIMA.Components;

public class Display
{
    public readonly Register DC = new("DC"); // Display Color
    public readonly Register DX = new("DX"); // Display x-pos
    public readonly Register DY = new("DY"); // Display y-pos

    private DisplayControl _displayControl;

    public void SetDisplay(DisplayControl displayControl)
    {
        _displayControl = displayControl;
    }

    public void SetPixel()
    {
        var colorIndex = DC.GetValueWithoutNotification();
        var x = DX.GetValueWithoutNotification();
        var y = DY.GetValueWithoutNotification();

        var color = (DisplayColor)colorIndex;

        Dispatcher.UIThread.InvokeAsync(() => _displayControl.Pixels[(x, y)].Background = GetBrush(color));
    }

    private IBrush GetBrush(DisplayColor color)
    {
        return color switch
        {
            DisplayColor.Red => Brushes.Red,
            DisplayColor.Green => Brushes.Green,
            DisplayColor.Blue => Brushes.Blue,
            DisplayColor.Yellow => Brushes.Yellow,
            DisplayColor.Cyan => Brushes.Cyan,
            DisplayColor.Magenta => Brushes.Magenta,
            DisplayColor.Orange => Brushes.Orange,
            DisplayColor.Purple => Brushes.Purple,
            DisplayColor.Pink => Brushes.Pink,
            DisplayColor.Brown => Brushes.Brown,
            DisplayColor.Gray => Brushes.Gray,
            DisplayColor.Black => Brushes.Black,
            DisplayColor.White => Brushes.White,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}