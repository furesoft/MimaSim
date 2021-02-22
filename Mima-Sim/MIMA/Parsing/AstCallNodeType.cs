﻿namespace MimaSim.MIMA.Parsing
{
    public enum AstCallNodeType
    {
        Group,
        Instruction,
        VariableAssignmentStatement,
        VariableDefinitionStatement,
        RegisterDefinitionStatement,
        IfStatement,
        BinaryExpresson,
        RegisterExpression,
        Label,
        MovRegReg,
        MovRegMem,
        MovMemReg,
        MovMemMem,
        NoArgInstruction,
        Load,
        Jmp,
        JmpConditional,
        LessThen,
        GreaterThan,
        And,
        Xor,
        Or,
        Division,
        Not,
        Multiplication,
        Subtraktion,
        Addition,
        Equal,
        NotEqual,
        LessEqual,
        GreaterEqual,
    }
}