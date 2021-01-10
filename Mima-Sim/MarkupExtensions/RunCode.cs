using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;
using MimaSim.Core.AST.Nodes;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Parsing.SourceTranslators;
using MimaSim.MIMA.Visitors;
using System;
using System.Diagnostics;

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
                   new RawSourceTextTranslator().ToRaw(txtBox.Text);

                   RegisterMap.GetRegister("IAR").SetValue(0xFF);

                   CPU.Instance.Step();
               }
           });
        }
    }
}