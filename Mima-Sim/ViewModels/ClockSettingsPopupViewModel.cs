using MimaSim.Commands;
using MimaSim.Core;
using System.Diagnostics;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ClockSettingsPopupViewModel : BaseViewModel
    {
        private ICommand _setClockSettingsCommand;

        public ICommand SetClockSettings
        {
            get { return _setClockSettingsCommand; }
            set { _setClockSettingsCommand = value; Raise(); }
        }

        public ClockSettingsPopupViewModel()
        {
            SetClockSettings = new DialogCommand(_ => { Debug.WriteLine("OK Called"); });
        }
    }
}