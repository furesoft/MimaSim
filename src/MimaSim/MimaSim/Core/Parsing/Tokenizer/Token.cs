namespace MimaSim.Core.Parsing.Tokenizer;

public sealed class Token
{
    public Token(TokenKind Kind, string contents, int start, int end)
    {
        this.Kind = Kind;
        Contents = contents;
        Start = start;
        End = end;
        Length = end - start;
    }

    public Token(TokenKind Kind, string contents)
    {
        this.Kind = Kind;
        Contents = contents;
        Length = contents.Length;
    }

    public static Token EndOfFile => new(TokenKind.EndOfFile, "");

    public string Contents { get; private set; }
    public int End { get; }
    public TokenKind Kind { get; private set; }
    public int Length { get; set; }
    public int Start { get; }

    public static bool IsTrivia(TokenKind Kind)
    {
        return Kind == TokenKind.Whitespace
               || Kind == TokenKind.Comment;
    }

    public override string ToString()
    {
        return "(" + Kind + ", \"" + Contents + "\")";
    }
}