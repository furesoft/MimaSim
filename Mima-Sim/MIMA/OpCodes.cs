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
        JMP = 0x30,

        JMPC = 0x31,

        CMPLT = 0x33,
        CMPLE = 0x34,
        CMPEQ = 0x35,
        CMPGE = 0x36,
        CMPGT = 0x37,

        MOV_REG_REG = 0x40,
        MOV_MEM_REG = 0x41,
        MOV_REG_MEM = 0x42,
        MOV_MEM_MEM = 0x43,

        //Only needed for parsing
        MOV = 0x44,
    }
}