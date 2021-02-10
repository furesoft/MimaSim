using MimaSim.Controls;
using MimaSim.Controls.OtherPopups;
using MimaSim.Core;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class FooterViewModel : BaseViewModel
    {
        private ICommand _openLicenseCommand;

        public ICommand OpenLicenseCommand
        {
            get { return _openLicenseCommand; }
            set { _openLicenseCommand = value; Raise(); }
        }

        public FooterViewModel()
        {
            OpenLicenseCommand = new DelegateCommand(_ => DialogService.Open(new LicensesPopupControl(), new LicensePopupViewModel()));
        }
    }
}