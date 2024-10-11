using Avalonia.Controls;
using Avalonia.ReactiveUI;
using MimaSim.Core;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Views;

public partial class MainView : ReactiveUserControl<ExecutionTabViewModel>
{
    public MainView()
    {
        ViewModel = new ExecutionTabViewModel(); // must be initialized before ui

        InitializeComponent();

        this.WhenActivated(_ =>
        {

        });
    }
}