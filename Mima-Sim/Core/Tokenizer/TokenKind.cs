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
        Operator,
        AddressLiteral,
        Colon,
        LabelReference,
    }
}