using System;
using MimaSim.Controls;
using MimaSim.MIMA.Components;
using ReactiveUI;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AvaloniaEdit.Highlighting;
using AvaloniaHex.Document;
using MimaSim.Core;

namespace MimaSim.ViewModels;

public class DisassemblyPopupViewModel : ReactiveObject, IActivatableViewModel
{
    private string? _source;
    private bool _showHexDump;
    private ByteArrayBinaryDocument _hexDocument;

    public DisassemblyPopupViewModel()
    {
        CloseCommand = ReactiveCommand.Create(DialogService.Close);
        ShowHexDump = false;

        Source = Disassemble();
        HexDocument = new ByteArrayBinaryDocument(CPU.Instance.Program, true);
    }

    public bool ShowHexDump
    {
        get => _showHexDump;
        set => this.RaiseAndSetIfChanged(ref _showHexDump, value);
    }

    public ViewModelActivator Activator => new();

    public ICommand CloseCommand { get; set; }

    public string? Source
    {
        get => _source;
        set => this.RaiseAndSetIfChanged(ref _source, value);
    }

    public ByteArrayBinaryDocument HexDocument
    {
        get => _hexDocument;
        set => this.RaiseAndSetIfChanged(ref _hexDocument, value);
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
        var stringBuilder = new StringBuilder();

        foreach (var item in raw)
        {
            var result = item.ToString("x");
            if (result.Length == 1)
            {
                result = "0" + result;
            }

            stringBuilder.Append(result.ToUpper());

            if ((stringBuilder.Length % 10) == 0)
            {
                stringBuilder.Append(Environment.NewLine);
            }
            else
            {
                stringBuilder.Append(' ');
            }
        }

        return stringBuilder.ToString().Trim();
    }
}