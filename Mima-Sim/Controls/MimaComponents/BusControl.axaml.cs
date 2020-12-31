using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
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

        private Orientation _orientation;

        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;

                if (_orientation == Orientation.Horizontal)
                {
                    Height = 25;
                }
                else if (_orientation == Orientation.Vertical)
                {
                    Width = 25;
                }
            }
        }

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