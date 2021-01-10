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
                return RegisterMap.GetRegisterValue(rc.Register);
            }

            return "empty";
        }
    }
}