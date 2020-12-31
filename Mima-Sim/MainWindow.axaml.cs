using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using MimaSim.Core;

namespace MimaSim
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            var contentGrid = this.FindControl<TabControl>("content");

            TabSwitcher.Initialize(contentGrid);
        }
    }
}