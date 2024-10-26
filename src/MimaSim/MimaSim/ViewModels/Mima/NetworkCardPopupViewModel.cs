using System.Windows.Input;
using MimaSim.Commands;
using MimaSim.Controls;
using MimaSim.Controls.MimaComponents.Popups;
using MimaSim.MIMA.Components;
using ReactiveUI;

namespace MimaSim.ViewModels.Mima;

public class NetworkCardPopupViewModel : ReactiveObject, IActivatableViewModel
{
    private string _mac;
    private string _ip;
    private string _subnetMask;

    public ViewModelActivator Activator { get; } = new ViewModelActivator();

    public string MAC
    {
        get => _mac;
        set => this.RaiseAndSetIfChanged(ref _mac, value);
    }

    public string IP
    {
        get => _ip;
        set => this.RaiseAndSetIfChanged(ref _ip, value);
    }

    public string SubnetMask
    {
        get => _subnetMask;
        set => this.RaiseAndSetIfChanged(ref _subnetMask, value);
    }

    public NetworkCardPopupViewModel()
    {
        MAC = CPU.Instance.NIC.MAC.ToString();
        IP = CPU.Instance.NIC.IP.ToString();
        SubnetMask = CPU.Instance.NIC.SubnetMask.ToString();
    }
}