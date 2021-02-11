using MimaSim.Controls;
using ReactiveUI;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ErrorPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        public ErrorPopupViewModel()
        {
            CloseCommand = ReactiveCommand.Create(() => DialogService.Close());
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { this.RaiseAndSetIfChanged(ref _message, value); }
        }

        public ICommand CloseCommand { get; set; }

        public ViewModelActivator Activator => new ViewModelActivator();
    }
}