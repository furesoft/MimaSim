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

                var byteRepOp = ((byte)Enum.Parse<OpCodes>(item, true)).ToString("X");
                if (byteRepOp.Length == 1)
                {
                    byteRepOp = byteRepOp.Insert(0, "0");
                }

                OpcodesTableItems.Add(item, byteRepOp);
            }
        }

        public ObservableDictionary<string, string> OpcodesTableItems { get; set; }
        public ObservableDictionary<string, string> RegisterTableItems { get; set; }
    }
}