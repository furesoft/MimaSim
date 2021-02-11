using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.MimaComponents.Popups
{
    public class MemoryPopupControl : ReactiveUserControl<MemoryPopupViewModel>
    {
        public MemoryPopupControl()
        {
            DataContext = new MemoryPopupViewModel();

            this.WhenActivated(disposables => { /* Handle view activation etc. */ });

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}