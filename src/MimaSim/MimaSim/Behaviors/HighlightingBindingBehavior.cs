using System;
using Avalonia;
using Avalonia.Xaml.Interactivity;
using AvaloniaEdit;
using AvaloniaEdit.Highlighting;

namespace MimaSim.Behaviors;

public class HighlightingBindingBehavior : Behavior<TextEditor>
{
    private TextEditor _textEditor;

    public static readonly StyledProperty<IHighlightingDefinition> HighlightingProperty =
        AvaloniaProperty.Register<HighlightingBindingBehavior, IHighlightingDefinition>(nameof(Highlighting));

    public IHighlightingDefinition Highlighting
    {
        get => GetValue(HighlightingProperty);
        set => SetValue(HighlightingProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is TextEditor textEditor)
        {
            _textEditor = textEditor;
            _textEditor.TextChanged += TextChanged;
            this.GetObservable(HighlightingProperty).Subscribe(HighlightingPropertyChanged);
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (_textEditor != null)
        {
            _textEditor.TextChanged -= TextChanged;
        }
    }

    private void TextChanged(object sender, EventArgs eventArgs)
    {
        if (_textEditor != null && _textEditor.Document != null)
        {
           Highlighting = _textEditor.SyntaxHighlighting;
        }
    }

    private void HighlightingPropertyChanged(IHighlightingDefinition highlighting)
    {
        if (_textEditor != null && _textEditor.Document != null && highlighting != null)
        {
            var caretOffset = _textEditor.CaretOffset;
            _textEditor.SyntaxHighlighting = highlighting;
        }
    }
}