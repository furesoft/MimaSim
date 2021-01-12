using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace MimaSim.Controls
{
    public class ContentDialog : ContentControl, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ContentDialog);

        public static AvaloniaProperty<object> DialogContentProperty =
            AvaloniaProperty.Register<ContentDialog, object>("DialogContent");

        public object DialogContent
        {
            get { return GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }
    }
}