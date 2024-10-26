namespace MimaSim.MIMA;

public enum Registers
{
    Invalid = 0,
    Accumulator,
    X,
    Y,
    IAR,
    SAR,
    SDR,
    SP,
    DC,
    DX,
    DY,
    FLAG,
    SCR // Register to store syscall result
}