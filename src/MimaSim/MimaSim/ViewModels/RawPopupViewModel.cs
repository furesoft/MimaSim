using MimaSim.Controls;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System.Linq;
using System.Windows.Input;

namespace MimaSim.ViewModels;

public class RawPopupViewModel : ReactiveObject, IActivatableViewModel
{
    private string _raw;
    private string _disassembly;

    public RawPopupViewModel()
    {
        CloseCommand = ReactiveCommand.Create(() => DialogService.Close());
        Raw = GetRawString();
        Disassembly = null;
    }

    public ViewModelActivator Activator => new();

    public ICommand CloseCommand { get; set; }

    public string Raw
    {
        get => _raw;
        set { this.RaiseAndSetIfChanged(ref _raw, value); }
    }

    public string Disassembly
    {
        get => _disassembly;
        set { this.RaiseAndSetIfChanged(ref _disassembly, value); }
    }

    private string GetRawString()
    {
        var raw = CPU.Instance.Program;

        return string.Join(' ', raw.Select(_ =>
        {
            var result = (_).ToString("x");
            if (result.Length == 1)
            {
                result = "0" + result;
            }

            return result;
        })).ToUpper();
    }
}