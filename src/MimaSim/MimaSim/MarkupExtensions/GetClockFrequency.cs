using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using MimaSim.Controls.MimaComponents;
using MimaSim.MIMA.Components;

namespace MimaSim.MarkupExtensions;

public class GetClockFrequency : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var ipv = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

        if (ipv.TargetObject is ClockControl clock)
        {
            CPU.Instance.Clock.FrequencyChanged += (_) =>
            {
                Dispatcher.UIThread.Invoke(() =>
                {
                    ToolTip.SetTip(clock, _);
                });
            };

            return CPU.Instance.Clock.Frequency;
        }

        return "0";
    }
}