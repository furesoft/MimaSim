using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.Parsing.SourceTranslators;
using System;

namespace MimaSim.MarkupExtensions
{
    public class RunCode : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var ipvt = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (ipvt.TargetObject is Button btn)
            {
                return new DelegateCommand(_ =>
               {
                   if (_ is TextBox txtBox && !string.IsNullOrEmpty(txtBox.Text))
                   {
                       var raw = new RawSourceTextTranslator().ToRaw(txtBox.Text); //ToDo: select source translator based on selection

                       CPU.Instance.Program = raw;
                       RegisterMap.GetRegister("IAR").SetValue(0);

                       CPU.Instance.Clock.Start();
                   }
               });
            }

            return null;
        }
    }
}