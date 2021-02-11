using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.MIMA.Components;
using System;
using System.Linq;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    public class RawViewModel : BaseViewModel
    {
        private string _raw;

        public string Raw
        {
            get { return _raw; }
            set { _raw = value; Raise(); }
        }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return _closeCommand; }
            set { _closeCommand = value; Raise(); }
        }

        public RawViewModel()
        {
            CloseCommand = new DelegateCommand(_ => DialogService.Close());
        }

        private string GetRawString()
        {
            var raw = CPU.Instance.Program;

            return string.Join(' ', raw.Select(_ =>
            {
                return (_).ToString("x");
            })).ToUpper();
        }

        public override void OnOpen()
        {
            Raw = GetRawString();
        }
    }
}