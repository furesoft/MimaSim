using MimaSim.Controls;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System.Linq;
using System.Windows.Input;
using MimaSim.Core;

namespace MimaSim.ViewModels;

public class DisassemblyPopupViewModel : ReactiveObject, IActivatableViewModel
{
    private string _raw;

    public DisassemblyPopupViewModel()
    {
        CloseCommand = ReactiveCommand.Create(() => DialogService.Close());
        Raw = GetRawString();
    }

    public ViewModelActivator Activator => new();

    public ICommand CloseCommand { get; set; }

    public string Raw
    {
        get => _raw;
        set { _raw = value; this.RaiseAndSetIfChanged(ref _raw, value); }
    }

    private string GetRawString()
    {
        var raw = CPU.Instance.Program;
        var disassembler = new Disassembler(raw);

        return disassembler.Disassemble();
    }
}