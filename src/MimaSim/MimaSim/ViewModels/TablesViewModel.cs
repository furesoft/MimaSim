using MimaSim.Core;
using MimaSim.MIMA;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MimaSim.Controls;
using MimaSim.Models;

namespace MimaSim.ViewModels;

public class TablesViewModel : ReactiveObject
{
    public ICommand CloseCommand { get; set; }

    public TablesViewModel()
    {
        CloseCommand = ReactiveCommand.Create(DialogService.Close);

        RegisterTableItems = [];
        OpcodesTableItems = [];
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

    public ObservableCollection<TableCellModel> OpcodesTableItems { get; }
    public ObservableCollection<TableCellModel> RegisterTableItems { get; }
    public ObservableCollection<TableCellModel> MovTableItems { get; }
}