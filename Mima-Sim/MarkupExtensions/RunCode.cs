using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;
using MimaSim.ViewModels;
using System;

namespace MimaSim.MarkupExtensions
{
    public class RunCode : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var ipvt = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (ipvt.TargetObject is ToggleButton btn)
            {
                return new DelegateCommand(_ =>
               {
                   var dataContext = (ExecutionTabViewModel)btn.DataContext;

                   if (!string.IsNullOrEmpty(dataContext.Source))
                   {
                       if (dataContext.RunMode)
                       {
                           var translator = SourceTextTranslatorSelector.Select((LanguageName)Enum.Parse(typeof(LanguageName), ((ComboBoxItem)dataContext.SelectedLanguage).Content.ToString()));

                           CPU.Instance.Program = translator.ToRaw(dataContext.Source, out var hasError);

                           if (hasError)
                           {
                               DialogService.OpenError("Der Programmcode enthält einige Fehler. Code kann nicht übersetzt werden.");
                           }

                           RegisterMap.GetRegister("IAR").SetValue(0);

                           CPU.Instance.Clock.Start();
                       }
                       else
                       {
                           CPU.Instance.Clock.Stop();
                       }
                   }
                   else
                   {
                       DialogService.OpenError("Bitte einen Programmtext eingeben. Dieser darf nicht leer sein!");
                   }
               });
            }

            return null;
        }
    }
}