using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;

namespace MimaSim.Controls.MimaComponents
{
    public class ClockControl : ReactiveUserControl<ExecutionTabViewModel>
    {
        public ClockControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}