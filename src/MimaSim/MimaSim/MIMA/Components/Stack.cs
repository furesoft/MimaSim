using MimaSim.Core;

namespace MimaSim.MIMA.Components;

public class Stack(CPU cpu)
{
    public short[] Data = new short[50];
    public Bus Bus = new Bus();

    public void Push()
    {
        var sp = cpu.ControlUnit.SP.GetValue();
        var value = cpu.Accumulator.GetValue();

        if (sp < 0)
        {
            cpu.ControlUnit.SetError(ErrorCodes.StackOverflow);
            return;
        }

        Data[sp] = value;
        sp--;

        cpu.ControlUnit.SP.SetValue(sp);
        Bus.Send((StackAction.Push, value));

        BusRegistry.Activate("cu->stack");
    }

    public void Pop()
    {
        var sp = cpu.ControlUnit.SP.GetValue();
        var value = Data[sp];

        if (sp >= 50)
        {
            cpu.ControlUnit.SetError(ErrorCodes.StackOverflow);
            return;
        }

        cpu.Accumulator.SetValue(value);

        sp++;
        cpu.ControlUnit.SP.SetValue(sp);

        BusRegistry.Activate("cu->stack");
        Bus.Send((StackAction.Pop, value));
    }
}