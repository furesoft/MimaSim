using MimaSim.Controls;
using MimaSim.Core;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class LicensePopupViewModel : BaseViewModel
    {
        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; Raise(); }
        }

        public LicensePopupViewModel()
        {
            CloseCommand = new DelegateCommand(_ => DialogService.Close());
        }
    }
}