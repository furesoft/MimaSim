using Avalonia.Markup.Xaml;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MimaSim.Core
{
    public abstract class BaseViewModel : MarkupExtension, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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