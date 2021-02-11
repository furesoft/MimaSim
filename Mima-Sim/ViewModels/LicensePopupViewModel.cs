using MimaSim.Controls;
using ReactiveUI;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class LicensePopupViewModel : ReactiveObject, IActivatableViewModel
    {
        public ICommand CloseCommand { get; set; }

        public ViewModelActivator Activator => new ViewModelActivator();

        public LicensePopupViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => DialogService.Close());
        }
    }
}