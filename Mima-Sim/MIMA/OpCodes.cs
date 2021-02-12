﻿namespace MimaSim.Core
{
    public enum OpCodes : byte
    {
        MOV_LIT_REG = 0x10,
        MOV_REG_REG = 0x11,
        MOV_REG_MEM = 0x12,
        MOV_MEM_REG = 0x13,
        MOV_LIT_MEM = 0x1B,
        MOV_REG_PTR_REG = 0x1C,
        MOV_LIT_OFF_REG = 0x1D,

        ADD_REG_REG = 0x14,
        ADD_LIT_REG = 0x3F,

        SUB_LIT_REG = 0x16,
        SUB_REG_LIT = 0x1E,
        SUB_REG_REG = 0x1F,

        MUL_LIT_REG = 0x20,
        MUL_REG_REG = 0x21,

        LSF_REG_LIT = 0x26,
        LSF_REG_REG = 0x27,

        RSF_REG_LIT = 0x2A,
        RSF_REG_REG = 0x2B,

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

        AND_REG_LIT = 0x2E,
        AND_REG_REG = 0x2F,
        OR_REG_LIT = 0x30,
        OR_REG_REG = 0x31,
        XOR_REG_LIT = 0x32,
        XOR_REG_REG = 0x33,
        NOT = 0x34,
    }
}