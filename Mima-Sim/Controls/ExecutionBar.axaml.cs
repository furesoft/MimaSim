using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;

namespace MimaSim.Controls
{
    public class ExecutionBar : ReactiveUserControl<ExecutionTabViewModel>
    {
        public ExecutionBar()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}