using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using MimaSim.Controls.MimaComponents;
using MimaSim.MIMA;
using System;
using System.Text;

namespace MimaSim.MarkupExtensions
{
    public class GetRegisterValue : MarkupExtension
    {
        public string Description { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var ipv = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (ipv.TargetObject is RegisterControl rc)
            {
                RegisterMap.GetRegister(rc.Register).Bus.Subscribe(_ =>
                {
                    ToolTip.SetTip(rc, _);
                });

                var sb = new StringBuilder();

                if (!string.IsNullOrEmpty(Description))
                {
                    sb.Append(Description).Append(" :");
                }

                var value = RegisterMap.GetRegister(rc.Register).GetValue();
                if (value != null)
                {
                    sb.Append(value);
                }
                else
                {
                    return "Kein Inhalt";
                }

                return sb.ToString();
            }

            return "0";
        }
    }
}