using System.Diagnostics;
using System.Windows.Input;
using MimaSim.Commands;
using MimaSim.MIMA.Components;
using ReactiveUI;

namespace MimaSim.ViewModels.Mima;

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