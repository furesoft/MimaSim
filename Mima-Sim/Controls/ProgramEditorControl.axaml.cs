using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MimaSim.Controls
{
    public class ProgramEditorControl : UserControl
    {
        public ProgramEditorControl()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            Initialized += ProgramEditorControl_Initialized;
        }

        private void ProgramEditorControl_Initialized(object sender, System.EventArgs e)
        {
            var cb = this.Find<ComboBox>("languageCB");

            cb.SelectedIndex = 0;
        }
    }
}