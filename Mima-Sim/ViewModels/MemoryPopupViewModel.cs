using Avalonia.Threading;
using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.Messages;
using ReactiveUI;
using System.Reactive;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class MemoryPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        public MemoryPopupViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => DialogService.Close());

            MemoryCells = new ObservableDictionary<int, ushort>();

            var observable = MessageBus.Current.Listen<MemoryCellChangedMessage>();
            observable.Subscribe(Observer.Create<MemoryCellChangedMessage>(_ =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (MemoryCells.ContainsKey(_.Address))
                    {
                        MemoryCells[_.Address] = _.Value;
                    }
                    else
                    {
                        MemoryCells.Add(_.Address, _.Value);
                    }
                });
            }));
        }

        public ViewModelActivator Activator => new ViewModelActivator();
        public ICommand CloseCommand { get; set; }

        public ObservableDictionary<int, ushort> MemoryCells { get; set; }
    }
}