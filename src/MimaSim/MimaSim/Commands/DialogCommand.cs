using MimaSim.Controls;
using System;
using System.Windows.Input;

namespace MimaSim.Commands;

public class DialogCommand(Action<object> command) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public Action<object> Command { get; set; } = command;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        Command?.Invoke(parameter);

        DialogService.Close();
    }
}