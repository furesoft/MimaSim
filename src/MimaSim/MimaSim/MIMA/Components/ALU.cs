namespace MimaSim.MIMA.Components;

public class ALU(CPU cPu)
{
    public Bus CpuBus = new();
    public Bus LeftInputBus = new();
    public Bus OutputBus = new();
    public Bus RightInputBus = new();
    private readonly CPU _cpu = cPu;
}