﻿using System;
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

public partial class ProgramEditorControl : ReactiveUserControl<ExecutionTabViewModel>
{
    public ProgramEditorControl()
    {
        this.WhenActivated(disposables => { /* Handle view activation etc. */ });

        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);

        InitHighlighting();
    }

    private void InitHighlighting()
    {
        LoadHighlighting("Maschinencode", ".hex", "Highligting_Hex");
        LoadHighlighting("Assembler", ".asm", "Highligting_Assembler");
        LoadHighlighting("Hochsprache", ".hoch", "Highligting_Hochsprache");
    }

    private IHighlightingDefinition LoadHighlighting(string name, string extension, string filename)
    {
        IHighlightingDefinition customHighlighting;
        using (Stream s = GetType().Assembly.GetManifestResourceStream($"MimaSim.Resources.{filename}.xshd"))
        {
            using (XmlReader reader = new XmlTextReader(s))
            {
                customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }
        }
    
        HighlightingManager.Instance.RegisterHighlighting(name, new string[] { extension }, customHighlighting);

        return customHighlighting;
    }
}