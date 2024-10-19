using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        ViewModel = new MainViewModel(); // must be initialized before ui

        InitializeComponent();

        this.WhenActivated(_ => { });
    }
}