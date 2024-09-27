namespace MimaSim.MIMA.Components;

public class ALU
{
    public Bus CpuBus = new();
    public Bus LeftInputBus = new();
    public Bus OutputBus = new();
    public Bus RightInputBus = new();
    private readonly CPU _cpu;

    public ALU(CPU cPU)
    {
        this._cpu = cPU;
    }
}