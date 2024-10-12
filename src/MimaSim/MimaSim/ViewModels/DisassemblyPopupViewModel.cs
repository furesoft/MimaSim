using MimaSim.Controls;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System.Linq;
using System.Windows.Input;
using AvaloniaEdit.Highlighting;
using MimaSim.Core;

namespace MimaSim.ViewModels;

public partial class DisassemblyPopupViewModel : ReactiveObject, IActivatableViewModel
{
    private string? _source;

    private bool _showHexDump;

    public DisassemblyPopupViewModel()
    {
        CloseCommand = ReactiveCommand.Create(DialogService.Close);
        ShowHexDump = false;
    }

    public bool ShowHexDump
    {
        get => _showHexDump;
        set
        {
            this.RaiseAndSetIfChanged(ref _showHexDump, value);

            Source = value ? GetRawString() : Disassemble();
        }
    }

    public ViewModelActivator Activator => new();

    public ICommand CloseCommand { get; set; }

    public string? Source
    {
        get => _source;
        set => this.RaiseAndSetIfChanged(ref _source, value);
    }

    private static string Disassemble()
    {
        var raw = CPU.Instance.Program;
        var disassembler = new Disassembler(raw);

        return disassembler.Disassemble();
    }

    private static string GetRawString()
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