using MimaSim.Commands;
using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using MimaSim.MIMA.Components;
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

        private ICommand _openMemoryPopupCommand;

        public ICommand OpenMemoryPopupCommand
        {
            get { return _openMemoryPopupCommand; }
            set { _openMemoryPopupCommand = value; Raise(); }
        }

        public ExecutionTabViewModel()
        {
            OpenErrorPopupCommand = new DelegateCommand(_ => DialogService.Open());

            OpenClockSettingsCommand = DialogService.CreateOpenCommand(new ClockSettingsPopupControl(), new ClockSettingsPopupViewModel());

            StepCommand = new DelegateCommand(_ => CPU.Instance.Step());
            StopCommand = new DelegateCommand(_ => CPU.Instance.Clock.Stop());

            ViewRawCommand = DialogService.CreateOpenCommand(new RawViewPopupControl(), new RawViewModel());
            OpenMemoryPopupCommand = DialogService.CreateOpenCommand(new MemoryPopupControl(), new MemoryPopupViewModel());
        }

        private string _source;

        public string Source
        {
            get { return _source; }
            set { _source = value; Raise(); }
        }

        private object _selectedLanguage;

        public object SelectedLanguage
        {
            get { return _selectedLanguage; }
            set { _selectedLanguage = value; Raise(); }
        }

        private bool _pauseMode;

        public bool RunMode
        {
            get { return _pauseMode; }
            set { _pauseMode = value; Raise(); }
        }

        private ICommand _stepCommand;

        public ICommand StepCommand
        {
            get { return _stepCommand; }
            set { _stepCommand = value; Raise(); }
        }

        private ICommand _stopCommand;

        public ICommand StopCommand
        {
            get { return _stopCommand; }
            set { _stopCommand = value; Raise(); }
        }

        private ICommand _viewRawCommand;

        public ICommand ViewRawCommand
        {
            get { return _viewRawCommand; }
            set { _viewRawCommand = value; Raise(); }
        }
    }
}