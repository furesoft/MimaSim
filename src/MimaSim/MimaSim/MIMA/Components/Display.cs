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

    public int Width => _displayControl.PixelWidth; // width in display pixel
    public int Height => _displayControl.PixelHeight; // height in display pixel

    public Font Font = new();

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

    public void DrawChar(char ch)
    {
        var xOffset = CPU.Instance.Display.DX.GetValueWithoutNotification();
        var yOffset = CPU.Instance.Display.DY.GetValueWithoutNotification();

        Font.DrawChar(xOffset, yOffset, ch);
    }

    public void SetPixel(short x, short y)
    {
        var colorIndex = DC.GetValueWithoutNotification();
        var color = (DisplayColor)colorIndex;

        SetPixel(x, y, color);
    }

    public void SetPixel(short x, short y, DisplayColor color)
    {
        Dispatcher.UIThread.InvokeAsync(() => _displayControl.Pixels[(x, y)].Background = GetBrush(color));
    }

    public void Clear(DisplayColor color)
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            foreach (var pixel in _displayControl.Pixels)
            {
                pixel.Value.Background = GetBrush(color);
            }
        });
    }

    public void Clear()
    {
        var colorIndex = DC.GetValueWithoutNotification();
        var color = (DisplayColor)colorIndex;

        Clear(color);
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