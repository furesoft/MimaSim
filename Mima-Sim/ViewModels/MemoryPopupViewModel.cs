﻿using MimaSim.Controls;
using MimaSim.Messages;
using MimaSim.MIMA;
using MimaSim.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class MemoryPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        public MemoryPopupViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => DialogService.Close());

            MemoryCells = new ObservableCollection<MemoryCellModel>();

            this.WhenActivated(disposables =>
            {
                var observable = MessageBus.Current.Listen<MemoryCellChangedMessage>();
                observable.Subscribe(Observer.Create<MemoryCellChangedMessage>(_ =>
                {
                    MemoryCells.Add(new MemoryCellModel { Address = _.Address.Value, Value = _.Value });
                }));

                Disposable
                    .Create(() => { /* Handle deactivation */ })
                    .DisposeWith(disposables);
            });
        }

        public ViewModelActivator Activator => new ViewModelActivator();
        public ICommand CloseCommand { get; set; }

        public ObservableCollection<MemoryCellModel> MemoryCells { get; set; }
    }
}