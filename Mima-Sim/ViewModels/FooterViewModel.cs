using MimaSim.Controls;
using MimaSim.Controls.OtherPopups;
using ReactiveUI;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class FooterViewModel : ReactiveObject, IActivatableViewModel
    {
        public ICommand OpenLicenseCommand { get; set; }

        public ViewModelActivator Activator => new ViewModelActivator();

        public FooterViewModel()
        {
            OpenLicenseCommand = DialogService.CreateOpenCommand(new LicensesPopupControl(), new LicensePopupViewModel());
        }
    }
}