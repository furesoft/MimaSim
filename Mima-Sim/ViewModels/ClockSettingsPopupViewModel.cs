using MimaSim.Commands;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System.Diagnostics;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ClockSettingsPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public ICommand SetClockSettings { get; set; }

        public ClockSettingsPopupViewModel()
        {
            SetClockSettings = new DialogCommand(_ => { Debug.WriteLine("OK Called"); });
        }

        public short Frequency
        {
            get { return CPU.Instance.Clock.Frequency; }
            set { CPU.Instance.Clock.SetFrequency(value); this.RaisePropertyChanged(); }
        }
    }
}