using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using MimaSim.Core;
using System;
using System.Windows.Input;

namespace MimaSim.Controls
{
    public class DialogControl : ContentControl, IStyleable
    {
        public static StyledProperty<ICommand> CancelCommandProperty =
            AvaloniaProperty.Register<DialogControl, ICommand>(nameof(CancelCommand));

        public static StyledProperty<bool> IsCancelEnabledProperty =
                    AvaloniaProperty.Register<DialogControl, bool>(nameof(IsCancelEnabled));

        public static StyledProperty<ICommand> CommandProperty =
            AvaloniaProperty.Register<DialogControl, ICommand>(nameof(Command));

        public static StyledProperty<string> CommandTextProperty =
            AvaloniaProperty.Register<DialogControl, string>(nameof(CommandText));

        public static StyledProperty<string> HeaderProperty =
                    AvaloniaProperty.Register<DialogControl, string>(nameof(Header));

        public ICommand CancelCommand
        {
            get { return GetValue<ICommand>(CancelCommandProperty); }
            set
            {
                SetValue(CancelCommandProperty, value);
            }
        }

        public DialogControl()
        {
            CancelCommand = new DelegateCommand(_ => DialogService.Close());
        }

        public bool IsCancelEnabled
        {
            get { return GetValue<bool>(IsCancelEnabledProperty); }
            set { SetValue(IsCancelEnabledProperty, value); }
        }

        public ICommand Command
        {
            get { return GetValue<ICommand>(CommandProperty); }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        public string CommandText
        {
            get { return GetValue<string>(CommandTextProperty); }
            set
            {
                SetValue(CommandTextProperty, value);
            }
        }

        public string Header
        {
            get { return GetValue<string>(HeaderProperty); }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        Type IStyleable.StyleKey => typeof(DialogControl);
    }
}