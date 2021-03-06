using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MimaSim.ViewModels;
using ReactiveUI;

namespace MimaSim.Controls
{
    public class ProgramEditorControl : ReactiveUserControl<ExecutionTabViewModel>
    {
        public ProgramEditorControl()
        {
            this.WhenActivated(disposables => { /* Handle view activation etc. */ });

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