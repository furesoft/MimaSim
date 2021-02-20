namespace MimaSim.MIMA
{
    public enum OpCodes
    {
        NOP = 0x00,
        EXIT = 0x01,

        LOAD = 0x04,

        //Arithmethic
        ADD = 0x08,

        MUL = 0x12,
        SUB = 0x13,
        DIV = 0x14,

        INC = 0x15,
        DEC = 0x16,

        //Shifting
        LSHIFT = 0x20,

        RSHIFT = 0x21,

        // Logical
        NOT = 0x24,

        AND = 0x25,
        OR = 0x26,
        XOR = 0x27,

        //Jumps
        JUMP = 0x30,

        JNEQ = 0x31,
        JEQ = 0x32,
        JLT = 0x33,
        JLE = 0x34,
        JGT = 0x35,
        JGE = 0x36,

        MOV_REG_REG = 0x40,
        MOV_MEM_REG = 0x41,
        MOV = 0x42,
    }
}