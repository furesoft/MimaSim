using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.MimaComponents.Popups
{
    public class RawViewPopupControl : ReactiveUserControl<RawPopupViewModel>
    {
        public RawViewPopupControl()
        {
            DataContext = new RawPopupViewModel();
            var cc = DataContext as IActivatableViewModel;
            cc.Activator.Activate();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}