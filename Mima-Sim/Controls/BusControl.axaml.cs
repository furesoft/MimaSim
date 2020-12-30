using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace MimaSim.Controls
{
    public enum BusState { None, Recieving }

    public class BusControl : UserControl
    {
        public BusControl()
        {
            this.InitializeComponent();
        }

        private BusState _state;

        public BusState State
        {
            get { return _state; }
            set
            {
                _state = value;

                if (_state == BusState.None)
                {
                    Background = Brushes.Gray;
                }
                else if (_state == BusState.Recieving)
                {
                    Background = Brushes.Red;
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}