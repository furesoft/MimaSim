using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;
using System.Reactive.Disposables;

namespace MimaSim.Controls.MimaComponents.Popups
{
    public class RawViewPopupControl : ReactiveUserControl<RawPopupViewModel>
    {
        public RawViewPopupControl()
        {
            DataContext = new RawPopupViewModel();

            this.WhenActivated(disposables =>
            {
                Disposable
                     .Create(() => { /* handle deactivation */ })
                     .DisposeWith(disposables);
            });

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}