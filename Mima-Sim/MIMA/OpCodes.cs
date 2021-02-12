namespace MimaSim.MIMA
{
    public enum OpCodes : byte
    {
        MOV_REG_REG = 0x11,
        MOV_REG_MEM = 0x12,
        MOV_MEM_REG = 0x13,

        MOV_REG_PTR_REG = 0x1C,

        Add = 0x14,

        Mul = 0x21,

        LSF = 0x27,

        RSH = 0x2B,

        INC_REG = 0x35,
        DEC_REG = 0x36,

        JNE_Lit = 0x15,
        JNE_REG = 0x40,
        JEQ_REG = 0x3E,
        JEQ_LIT = 0x41,
        JLT_REG = 0x42,
        JLT_LIT = 0x43,
        JGT_REG = 0x44,
        JGT_LIT = 0x45,
        JLE_REG = 0x46,
        JLE_LIT = 0x47,
        JGE_REG = 0x48,
        JGE_LIT = 0x49,

        Exit = 0x60,

        NOP = 0xFF,

        And = 0x2F,
        Or = 0x30,
        Xor = 0x32,
        NOT = 0x34,
        Div = 0x51,
        SUB = 0x50,
    }
}