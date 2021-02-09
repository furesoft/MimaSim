using Avalonia.Markup.Xaml;
using MimaSim.Controls;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MimaSim.Core
{
    public abstract class BaseViewModel : MarkupExtension, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CancelCommand = new DelegateCommand(_ => DialogService.Close());

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Activator.CreateInstance(GetType());
        }

        protected void Raise([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}