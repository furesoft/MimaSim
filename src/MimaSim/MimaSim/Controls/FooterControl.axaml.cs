using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls;

public partial class FooterControl : ReactiveUserControl<FooterViewModel>
{
    public FooterControl()
    {
        DataContext = new FooterViewModel();

        this.WhenActivated(disposables => { /* Handle view activation etc. */ });

        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}