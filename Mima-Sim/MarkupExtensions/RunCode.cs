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
        public string LangaugeSelector { get; set; }

        public RunCode(string langaugeSelector)
        {
            LangaugeSelector = langaugeSelector;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var ipvt = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
            var root = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
            if (ipvt.TargetObject is Button btn)
            {
                return new DelegateCommand(_ =>
               {
                   if (_ is TextBox txtBox && !string.IsNullOrEmpty(txtBox.Text))
                   {
                       var rootObj = (Control)root.RootObject;
                       var cbLanguage = rootObj.Find<ComboBox>(LangaugeSelector);
                       var item = (ComboBoxItem)cbLanguage.SelectedItem;

                       var translator = SourceTextTranslatorSelector.Select((LanguageName)Enum.Parse(typeof(LanguageName), item.Content.ToString()));
                       var raw = translator.ToRaw(txtBox.Text); //ToDo: select source translator based on selection

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