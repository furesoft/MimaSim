using MimaSim.Controls;
using ReactiveUI;
using System.Windows.Input;

namespace MimaSim.ViewModels;

public class LicensePopupViewModel : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator => new();
    public ICommand CloseCommand { get; set; } = ReactiveCommand.Create(() => DialogService.Close());
}