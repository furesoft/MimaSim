using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.Parsing.SourceTranslators;
using System;

namespace MimaSim.MarkupExtensions
{
    public class RunCode : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //ToDo: Get Emitter based on language selection
            return new DelegateCommand(_ =>
           {
               if (_ is TextBox txtBox && !string.IsNullOrEmpty(txtBox.Text))
               {
                   new RawSourceTextTranslator().ToRaw(txtBox.Text); //ToDo: select source translator based on selection

                   CPU.Instance.Step();
                   CPU.Instance.Clock.SetFrequency(2500);
               }
           });
        }
    }
}