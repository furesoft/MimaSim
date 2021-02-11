namespace MimaSim.Core.Tokenizer
{
    public sealed class Token
    {
        public Token(TokenKind Kind, string Contents, int length)
        {
            this.Kind = Kind;
            this.Contents = Contents;
            Length = length;
        }

        public Token(TokenKind Kind, string Contents)
        {
            this.Kind = Kind;
            this.Contents = Contents;
            Length = Contents.Length;
        }

        public static Token EndOfFile
        {
            get { return new Token(TokenKind.EndOfFile, ""); }
        }

        public string Contents { get; private set; }
        public TokenKind Kind { get; private set; }
        public int Length { get; set; }

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
}