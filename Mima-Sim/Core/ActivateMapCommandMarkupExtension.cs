using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Windows.Input;

namespace MimaSim.Core
{
    public class ActivateMapCommandMarkupExtension : MarkupExtension, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(parameter.ToString());
        }

        public void Execute(object parameter)
        {
            BusRegistry.GetBusMap(parameter.ToString()).Activate();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}