using System;
using System.IO;
using System.Xml;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaEdit;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls;

public partial class ProgramEditorControl : ReactiveUserControl<MainViewModel>
{
    public ProgramEditorControl()
    {
        this.WhenActivated(disposables => { /* Handle view activation etc. */ });

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}