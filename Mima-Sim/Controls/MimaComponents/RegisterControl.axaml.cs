using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.MimaComponents
{
    public class RegisterControl : UserControl
    {
        public static AvaloniaProperty<string> RegisterProperty =
                AvaloniaProperty.Register<RegisterControl, string>("Register");

        public RegisterControl()
        {
            this.InitializeComponent();
        }

        public string Register
        {
            get { return (string)GetValue(RegisterProperty); }
            set { SetValue(RegisterProperty, value); }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}