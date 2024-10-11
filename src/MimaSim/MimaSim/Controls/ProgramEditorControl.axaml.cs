using System;
using System.IO;
using System.Xml;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
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

        IHighlightingDefinition customHighlighting;
        using (Stream s = GetType().Assembly.GetManifestResourceStream("MimaSim.Resources.CustomHighligting.xshd"))
        {
            using (XmlReader reader = new XmlTextReader(s))
            {
                customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }
        }

        HighlightingManager.Instance.RegisterHighlighting("Custom Highlighting", new string[] { ".cool" }, customHighlighting);

        editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(".cool");
    }
}