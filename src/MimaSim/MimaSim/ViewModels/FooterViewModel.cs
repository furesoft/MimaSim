using MimaSim.Controls;
using MimaSim.Controls.OtherPopups;
using ReactiveUI;
using System.Windows.Input;

namespace MimaSim.ViewModels;

public class FooterViewModel : ReactiveObject, IActivatableViewModel
{
    public FooterViewModel()
    {
        OpenLicenseCommand = DialogService.CreateOpenCommand(new LicensesPopupControl(), new LicensePopupViewModel());
    }

    public ViewModelActivator Activator => new();
    public ICommand OpenLicenseCommand { get; set; }
}