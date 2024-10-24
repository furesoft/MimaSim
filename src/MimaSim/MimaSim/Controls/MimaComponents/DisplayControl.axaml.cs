using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using MimaSim.MIMA.Components;

namespace MimaSim.Controls.MimaComponents;

public partial class DisplayControl : UserControl
{
    public Dictionary<(int, int), Border> Pixels { get; set; } = [];

    public DisplayControl()
    {
        InitializeComponent();
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        var grid = this.Find<Grid>("grid");
        CPU.Instance.Display.SetDisplay(this);

        var stack = new StackPanel()
        {
            Spacing = 0,
            Orientation = Orientation.Vertical
        };

        for (int row = 0; row < 13; row++)
        {
            var rowStack = new StackPanel()
            {
                Spacing = 0,
                Orientation = Orientation.Horizontal
            };

            for (int col = 0; col < 55; col++)
            {
                var cell = new Border
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(0),
                    Width = 10,
                    Height = 10,
                    Background = Brushes.White
                };
                Pixels.Add((row, col), cell);

                rowStack.Children.Add(cell);
            }

            stack.Children.Add(rowStack);
        }

        grid.Children.Add(stack);

        base.OnApplyTemplate(e);
    }
}