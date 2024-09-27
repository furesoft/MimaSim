using MimaSim.Core;
using MimaSim.MIMA;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace MimaSim.ViewModels;

public class TablesViewModel : ReactiveObject
{
    public TablesViewModel()
    {
        RegisterTableItems = [];
        OpcodesTableItems = [];

        var registerNames = Enum.GetNames<Registers>();
        for (int i = 1; i < registerNames.Length; i++)
        {
            string item = registerNames[i];
            RegisterTableItems.Add((item, $"{(int)Enum.Parse<Registers>(item, true):D2}"));
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

            OpcodesTableItems.Add((item, byteRepOp));
        }
    }

    public ObservableCollection<(string Key, string Value)> OpcodesTableItems { get; }
    public ObservableCollection<(string Key, string Value)> RegisterTableItems { get; }
}