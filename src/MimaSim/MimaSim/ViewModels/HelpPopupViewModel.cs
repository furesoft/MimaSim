using MimaSim.Core;
using MimaSim.MIMA;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MimaSim.Controls;
using MimaSim.MIMA.Components;
using MimaSim.Models;

namespace MimaSim.ViewModels;

public class HelpPopupViewModel : ReactiveObject
{
    public ICommand CloseCommand { get; set; }

    public ObservableCollection<TableCellModel> OpcodesTableItems { get; }
    public ObservableCollection<TableCellModel> RegisterTableItems { get; }
    public ObservableCollection<TableCellModel> MovTableItems { get; }
    public ObservableCollection<TableCellModel> ColorCodes { get; }

    public HelpPopupViewModel()
    {
        CloseCommand = ReactiveCommand.Create(DialogService.Close);

        RegisterTableItems = [];
        OpcodesTableItems = [];
        ColorCodes = [];
        MovTableItems = [
            new("Reg -> Reg", "40"),
            new("Mem -> Reg", "41"),
            new("Reg -> Mem", "42"),
            new("Mem -> Mem", "43"),
        ];

        var registerNames = Enum.GetNames<Registers>();
        for (int i = 1; i < registerNames.Length; i++)
        {
            string item = registerNames[i];
            RegisterTableItems.Add(new(item, $"{(int)Enum.Parse<Registers>(item, true):D2}"));
        }

        var colorNames = Enum.GetNames<DisplayColor>();
        for (int i = 0; i < colorNames.Length; i++)
        {
            string item = colorNames[i];
            ColorCodes.Add(new(item, $"{(int)Enum.Parse<DisplayColor>(item, true):D2}"));
        }

        foreach (var item in Enum.GetNames<OpCodes>())
        {
            if (item.StartsWith("MOV"))
            {
                break;
            }

            var byteRepOp = ((byte)Enum.Parse<OpCodes>(item, true)).ToString("X");
            if (byteRepOp.Length == 1)
            {
                byteRepOp = byteRepOp.Insert(0, "0");
            }

            OpcodesTableItems.Add(new(item, byteRepOp));
        }
    }
}