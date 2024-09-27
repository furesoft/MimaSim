using System.Collections.ObjectModel;
using Avalonia.Threading;
using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.Messages;
using ReactiveUI;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using MimaSim.Models;

namespace MimaSim.ViewModels;

public class MemoryPopupViewModel : ReactiveObject, IActivatableViewModel
{
    public MemoryPopupViewModel()
    {
        CloseCommand = ReactiveCommand.Create(DialogService.Close);

        MemoryCells = [];

        var observable = MessageBus.Current.Listen<MemoryCellChangedMessage>();
        observable.Subscribe(Observer.Create<MemoryCellChangedMessage>(_ =>
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (MemoryCells.Any(m => _.Address == m.Address))
                {
                    MemoryCells[_.Address].Value = _.Value;
                }
                else
                {
                    MemoryCells.Add(new(_.Address, _.Value));
                }

                //MemoryCells.OrderBy(_ => _.Address);
            });
        }));
    }

    public ViewModelActivator Activator => new();
    public ICommand CloseCommand { get; set; }

    public ObservableCollection<MemoryCellModel> MemoryCells { get; set; }
}