using MimaSim.Controls;
using MimaSim.Core;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ErrorPopupViewModel : BaseViewModel
    {
        public ErrorPopupViewModel()
        {
            CloseCommand = new DelegateCommand(_ => DialogService.Close());
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; Raise(); }
        }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; Raise(); }
        }
    }
}