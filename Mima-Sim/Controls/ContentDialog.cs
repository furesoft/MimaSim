using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace MimaSim.Controls
{
    public class ContentDialog : ContentControl, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ContentDialog);

        public static StyledProperty<object> DialogContentProperty =
            AvaloniaProperty.Register<ContentDialog, object>("DialogContent");

        public static StyledProperty<bool> IsOpenedProperty =
            AvaloniaProperty.Register<ContentDialog, bool>("IsOpened");

        public bool IsOpened
        {
            get { return GetValue<bool>(IsOpenedProperty); }
            set { SetValue(IsOpenedProperty, value); }
        }

        public object DialogContent
        {
            get { return GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }
    }
}