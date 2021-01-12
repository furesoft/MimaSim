using MimaSim.Controls;
using MimaSim.Core;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ExecutionTabModel : BaseViewModel
    {
        private ICommand _openCommand;

        public ICommand OpenCommand
        {
            get { return _openCommand; }
            set { _openCommand = value; Raise(); }
        }

        public ExecutionTabModel()
        {
            _openCommand = new DelegateCommand(_ => DialogService.Open());
        }
    }
}