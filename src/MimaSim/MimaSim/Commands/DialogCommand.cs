using MimaSim.Controls;
using System;
using System.Windows.Input;

namespace MimaSim.Commands;

public class DialogCommand : ICommand
{
    public DialogCommand(Action<object> command)
    {
        Command = command;
    }

    public event EventHandler CanExecuteChanged;

    public Action<object> Command { get; set; }

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