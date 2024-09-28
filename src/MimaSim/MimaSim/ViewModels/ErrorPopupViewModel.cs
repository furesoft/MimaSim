using MimaSim.Controls;
using ReactiveUI;
using System.Windows.Input;

namespace MimaSim.ViewModels;

public class ErrorPopupViewModel : ReactiveObject, IActivatableViewModel
{
    private string? _message;

    public ViewModelActivator Activator => new();

    public ICommand CloseCommand { get; set; } = ReactiveCommand.Create(() => DialogService.Close());

    public string? Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }
}