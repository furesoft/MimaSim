using Avalonia.Controls;
using MimaSim.Controls.MimaComponents;
using MimaSim.MIMA;

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