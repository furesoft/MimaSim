using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Avalonia.Threading;
using MimaSim.Controls;
using MimaSim.Messages;
using MimaSim.MIMA.Components;
using MimaSim.Models;
using ReactiveUI;

namespace MimaSim.ViewModels.Mima;

public class MemoryPopupViewModel : ReactiveObject, IActivatableViewModel
{
    public MemoryPopupViewModel()
    {
        CloseCommand = ReactiveCommand.Create(DialogService.Close);

        MemoryCells = [];

        for (short i = 0; i < CPU.Instance.Memory.Length; i++)
        {
            MemoryCells.Add(new(i, 0));
        }

        var observable = MessageBus.Current.Listen<MemoryCellChangedMessage>();
        observable.Subscribe(Observer.Create<MemoryCellChangedMessage>(_ =>
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (MemoryCells.Any(m => _.Address == m.Address))
                {
                    MemoryCells[_.Address].Value = _.Value;
                }
            });
        }));
    }

    public ViewModelActivator Activator => new();
    public ICommand CloseCommand { get; set; }

    public ObservableCollection<MemoryCellModel> MemoryCells { get; set; }
}