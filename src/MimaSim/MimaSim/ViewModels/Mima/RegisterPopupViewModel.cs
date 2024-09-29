using System.Windows.Input;
using MimaSim.Commands;
using MimaSim.MIMA;
using ReactiveUI;

namespace MimaSim.ViewModels.Mima;

internal class RegisterPopupViewModel : ReactiveObject, IActivatableViewModel
{
    private short _value;

    public RegisterPopupViewModel(string register)
    {
        Register = register;
        RegisterName = "Register " + register;

        SetRegisterValueCommand = new DialogCommand(_ =>
        {

        });
    }

    public ViewModelActivator Activator => new();

    public string Register { get; set; }
    public string RegisterName { get; set; }
    public ICommand SetRegisterValueCommand { get; set; }

    public short Value
    {
        get => _value;
        set => this.RaiseAndSetIfChanged(ref _value, value);
    }
}