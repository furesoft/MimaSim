using Avalonia.Markup.Xaml;
using MimaSim.Controls;
using MimaSim.Controls.MimaComponents;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.MIMA;
using MimaSim.ViewModels;
using ReactiveUI;
using System;

namespace MimaSim.MarkupExtensions
{
    public class OpenRegisterPopup : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var ipv = (IRootObjectProvider)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (ipv.RootObject is RegisterControl reg)
            {
                return ReactiveCommand.Create(() =>
                {
                    DialogService.Open(new RegisterPopupControl(), new RegisterPopupViewModel(reg.Register)
                    {
                        Value = RegisterMap.GetRegister(reg.Register).GetValue()
                    });
                });
            }

            return null;
        }
    }
}