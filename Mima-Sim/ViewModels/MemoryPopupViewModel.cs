using MimaSim.Controls;
using MimaSim.Core;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class MemoryPopupViewModel : BaseViewModel
    {
        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; Raise(); }
        }

        public MemoryPopupViewModel()
        {
            CloseCommand = new DelegateCommand(_ => DialogService.Close());
        }
    }
}