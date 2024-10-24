﻿using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls.MimaComponents.Wrappers;

public partial class MemoryWrapper : ReactiveUserControl<MainViewModel>
{
    public MemoryWrapper()
    {
        this.WhenActivated(disposables =>
        {
            /* Handle view activation etc. */
        });

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}