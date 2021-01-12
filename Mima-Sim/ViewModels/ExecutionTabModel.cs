using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ExecutionTabModel : BaseViewModel
    {
        private ICommand _openErrorPopupCommand;

        public ICommand OpenErrorPopupCommand
        {
            get { return _openErrorPopupCommand; }
            set { _openErrorPopupCommand = value; Raise(); }
        }

        public ExecutionTabModel()
        {
            _openErrorPopupCommand = new DelegateCommand(_ => DialogService.Open(new ErrorPopupControl()));
        }
    }
}