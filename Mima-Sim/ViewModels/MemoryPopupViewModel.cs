using Avalonia.Threading;
using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.Messages;
using ReactiveUI;
using System.Linq;
using System.Reactive;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class MemoryPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        public MemoryPopupViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => DialogService.Close());

            MemoryCells = new();

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

                    MemoryCells.OrderBy(_ => _.Key);
                });
            }));
        }

        public ViewModelActivator Activator => new();
        public ICommand CloseCommand { get; set; }

        public ObservableDictionary<int, short> MemoryCells { get; set; }
    }
}