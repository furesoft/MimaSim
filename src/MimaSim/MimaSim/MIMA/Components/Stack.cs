namespace MimaSim.MIMA.Components;

public class Stack(CPU cpu)
{
    public short[] Data = new short[50];

    public void Push()
    {
        var sp = cpu.ControlUnit.SP.GetValue();
        var value = cpu.Accumulator.GetValue();

        Data[sp] = value;
        sp--;

        cpu.ControlUnit.SP.SetValue(sp);
    }

    public void Pop()
    {
        var sp = cpu.ControlUnit.SP.GetValue();
        var value = Data[sp];

        cpu.Accumulator.SetValue(value);

        sp++;
        cpu.ControlUnit.SP.SetValue(sp);
    }
}