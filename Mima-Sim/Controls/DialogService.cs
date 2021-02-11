using Avalonia.Controls;
using MimaSim.Commands;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using MimaSim.ViewModels;
using System;
using System.Windows.Input;

namespace MimaSim.Controls
{
    public static class DialogService
    {
        private static ContentDialog _host;

        public static void SetIsHost(ContentDialog target, bool value)
        {
            if (value)
            {
                _host = target;
            }
        }

        public static bool GetIsHost(ContentDialog target)
        {
            return object.ReferenceEquals(_host, target);
        }

        public static void Open(object content)
        {
            if (_host != null)
            {
                _host.DialogContent = content;
                _host.IsOpened = true;
            }
        }

        public static void Open(Control content, BaseViewModel viewModel)
        {
            content.DataContext = viewModel;

            Open(content);
        }

        public static void OpenError(string message)
        {
            Open(new ErrorPopupControl(), new ErrorPopupViewModel { Message = message });
        }

        public static void Open()
        {
            if (_host != null)
            {
                _host.IsOpened = true;
            }
        }

        public static void Close()
        {
            if (_host != null)
            {
                _host.IsOpened = false;
            }
        }

        public static ICommand CreateOpenCommand(Control content, BaseViewModel viewModel)
        {
            return new DelegateCommand(_ =>
            {
                viewModel.OnOpen();
                DialogService.Open(content, viewModel);
            });
        }
    }
}