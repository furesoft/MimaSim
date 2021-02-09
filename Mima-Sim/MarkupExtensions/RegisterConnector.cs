using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using MimaSim.Controls.MimaComponents;
using MimaSim.MIMA;
using System;
using System.Text;

namespace MimaSim.MarkupExtensions
{
    public static class RegisterConnector
    {
        public static void SetIsConnected(RegisterControl rc, bool value)
        {
            if (value)
            {
                RegisterMap.GetRegister(rc.Register).Bus.Subscribe(_ => ToolTip.SetTip(rc, _));
            }
        }
    }
}