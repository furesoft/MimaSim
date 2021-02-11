using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;

namespace MimaSim.Controls.MimaComponents.Wrappers
{
    public class MemoryWrapper : ReactiveUserControl<ExecutionTabViewModel>
    {
        public MemoryWrapper()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}