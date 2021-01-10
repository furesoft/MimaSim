using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using MimaSim.Controls.MimaComponents;
using MimaSim.MIMA;
using System;

namespace MimaSim.MarkupExtensions
{
    public class GetRegisterValue : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var ipv = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (ipv.TargetObject is RegisterControl rc)
            {
                RegisterMap.GetRegister(rc.Register).Bus.Subscribe(_ =>
                {
                    ToolTip.SetTip(rc, _);
                });

                return RegisterMap.GetRegister(rc.Register).GetValue();
            }

            return "0";
        }
    }
}