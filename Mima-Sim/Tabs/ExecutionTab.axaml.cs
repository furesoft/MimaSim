using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.Core;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Tabs
{
    public class ExecutionTab : ReactiveUserControl<ExecutionTabViewModel>, IUITab
    {
        public ExecutionTab()
        {
            DataContext = new ExecutionTabViewModel();

            this.WhenActivated(disposables => { /* Handle view activation etc. */ });

            this.InitializeComponent();
        }

        public int Index => 1;
        public string Title => "Prozessor";

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}