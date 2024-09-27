using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace MimaSim.Controls;

public class MapTableControl : ItemsControl, IStyleable
{
    public static readonly StyledProperty<string> LeftHeaderProperty =
        AvaloniaProperty.Register<MapTableControl, string>(nameof(LeftHeader));

    public static readonly StyledProperty<string> RightHeaderProperty =
        AvaloniaProperty.Register<MapTableControl, string>(nameof(RightHeader));

    public string LeftHeader
    {
        get => GetValue(LeftHeaderProperty);
        set => SetValue(LeftHeaderProperty, value);
    }

    public string RightHeader
    {
        get => GetValue(RightHeaderProperty);
        set => SetValue(RightHeaderProperty, value);
    }

    Type IStyleable.StyleKey => typeof(MapTableControl);
}