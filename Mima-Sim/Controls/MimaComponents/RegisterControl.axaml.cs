using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls.MimaComponents
{
    public class RegisterControl : UserControl
    {
        public static AvaloniaProperty<string> RegisterProperty =
                AvaloniaProperty.Register<RegisterControl, string>("Register");

        public string Register
        {
            get { return GetValue(RegisterProperty); }
            set { SetValue(RegisterProperty, value); }
        }

        public RegisterControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}