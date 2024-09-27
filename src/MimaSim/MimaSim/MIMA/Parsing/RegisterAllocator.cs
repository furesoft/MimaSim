namespace MimaSim.MIMA.Parsing;

public class RegisterAllocator
{
    private bool IsXUsed = false;

    public Registers Allocate()
    {
        if (IsXUsed)
        {
            IsXUsed = false;
            return Registers.Y;
        }
        else
        {
            IsXUsed = true;
            return Registers.X;
        }
    }
}