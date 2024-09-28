using MimaSim.Commands;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System.Diagnostics;
using System.Windows.Input;

namespace MimaSim.ViewModels;

public class ClockSettingsPopupViewModel : ReactiveObject, IActivatableViewModel
{
    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    public short Frequency
    {
        get => CPU.Instance.Clock.Frequency;
        set { CPU.Instance.Clock.SetFrequency(value); this.RaisePropertyChanged(); }
    }

    public ICommand SetClockSettings { get; set; } = new DialogCommand(_ => { Debug.WriteLine("OK Called"); });
}