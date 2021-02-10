using MimaSim.Core;
using MimaSim.MIMA.Components;
using System;
using System.Linq;

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

        public RawViewModel()
        {
            Raw = GetRawString();
        }

        private string GetRawString()
        {
            var raw = CPU.Instance.Program;

            return string.Join(' ', raw.Select(_ =>
            {
                return (_).ToString("x");
            })).ToUpper();
        }
    }
}