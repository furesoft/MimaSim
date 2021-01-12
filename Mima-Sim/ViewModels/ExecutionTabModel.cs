using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.Core;
using System;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class ExecutionTabModel : BaseViewModel
    {
        private ICommand _openErrorPopupCommand;

        public ICommand OpenErrorPopupCommand
        {
            get { return _openErrorPopupCommand; }
            set { _openErrorPopupCommand = value; Raise(); }
        }

        private ICommand _testCommand;

        public ICommand TestCommand
        {
            get { return _testCommand; }
            set { _testCommand = value; Raise(); }
        }

        public ExecutionTabModel()
        {
            OpenErrorPopupCommand = new DelegateCommand(_ => DialogService.Open());
            TestCommand = new DelegateCommand(_ =>
            {
                Environment.Exit(0);
            });
        }
    }
}