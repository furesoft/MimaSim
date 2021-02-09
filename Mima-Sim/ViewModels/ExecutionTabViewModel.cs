using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ExecutionTabViewModel : BaseViewModel
    {
        private ICommand _openErrorPopupCommand;

        public ICommand OpenErrorPopupCommand
        {
            get { return _openErrorPopupCommand; }
            set { _openErrorPopupCommand = value; Raise(); }
        }

        private ICommand _openClockSettingsCommand;

        public ICommand OpenClockSettingsCommand
        {
            get { return _openClockSettingsCommand; }
            set { _openClockSettingsCommand = value; Raise(); }
        }

        public ExecutionTabViewModel()
        {
            OpenErrorPopupCommand = new DelegateCommand(_ => DialogService.Open());

            OpenClockSettingsCommand = DialogService.CreateCommand(new ClockSettingsPopupControl(), new ClockSettingsPopupViewModel());
        }
    }
}