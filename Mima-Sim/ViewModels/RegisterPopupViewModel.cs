using MimaSim.Commands;
using MimaSim.MIMA;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace MimaSim.ViewModels
{
    internal class RegisterPopupViewModel : ReactiveObject, IActivatableViewModel
    {
        private short _value;

        public RegisterPopupViewModel(string register)
        {
            Register = register;
            RegisterName = "Register " + register;

            SetRegisterValueCommand = new DialogCommand((_) =>
            {
                RegisterMap.GetRegister(Register).SetValue(Value);
            });
        }

        public ViewModelActivator Activator => throw new NotImplementedException();

        public string Register { get; set; }
        public string RegisterName { get; set; }
        public ICommand SetRegisterValueCommand { get; set; }

        public short Value
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }
    }
}