namespace MimaSim.Core.Tokenizer
{
    public enum TokenKind
    {
        Undefined,

        EndOfFile,

        Whitespace,

        Comment,

        Identifier,

        HexLiteral,

        Mnemnonic,
        Register,
        Comma,
        Uppersand,
        Number,

        OpenBracket,
        CloseBracket,
        CloseParen,
        OpenParen,
        Exclamation,

        AddressLiteral,
        Colon,
        LabelReference,
        IntLiteral,
        FalseKeyword,
        TrueKeyword,
        Slash,
        Star,
        Minus,
        Plus,
        Operator,
        Pipe,
        Hat,
        Bang,
        Ampersand,
        GreaterThen,
        LessThen,
        EqualsEquals,
        NotEquals,
        LessEquals,
        GreaterEquals,
        Unknown,
        IfKeyword,
        RegisterKeyword,
        EqualsToken,
        VarKeyword,
    }
}