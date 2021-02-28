using MimaSim.Core;
using MimaSim.MIMA;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace MimaSim.ViewModels
{
    public class TablesViewModel : ReactiveObject
    {
        public TablesViewModel()
        {
            RegisterTableItems = new ObservableDictionary<string, string>();
            OpcodesTableItems = new ObservableDictionary<string, string>();

            string[] registerNames = Enum.GetNames<Registers>();
            for (int i = 1; i < registerNames.Length; i++)
            {
                string item = registerNames[i];
                RegisterTableItems.Add(item, string.Format("{0:D2}", (int)Enum.Parse<Registers>(item, true)));
            }

            foreach (var item in Enum.GetNames<OpCodes>())
            {
                if (item.StartsWith("MOV"))
                {
                    break;
                }

                OpcodesTableItems.Add(item, string.Format("{0:D2}", (int)Enum.Parse<OpCodes>(item, true)));
            }
        }

        public ObservableDictionary<string, string> OpcodesTableItems { get; set; }
        public ObservableDictionary<string, string> RegisterTableItems { get; set; }
    }
}