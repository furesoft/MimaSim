using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using MimaSim.MIMA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MimaSim.Controls
{
    public class MapTableControl : ItemsControl, IStyleable
    {
        public static StyledProperty<string> LeftHeaderProperty =
            AvaloniaProperty.Register<MapTableControl, string>(nameof(LeftHeader));

        public static StyledProperty<string> RightHeaderProperty =
            AvaloniaProperty.Register<MapTableControl, string>(nameof(RightHeader));

        public string LeftHeader
        {
            get { return GetValue<string>(LeftHeaderProperty); }
            set
            {
                SetValue(LeftHeaderProperty, value);
            }
        }

        public string RightHeader
        {
            get { return GetValue<string>(RightHeaderProperty); }
            set
            {
                SetValue(RightHeaderProperty, value);
            }
        }

        Type IStyleable.StyleKey => typeof(MapTableControl);
    }
}