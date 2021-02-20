﻿namespace MimaSim.Core.Tokenizer
{
    public sealed class Token
    {
        public Token(TokenKind Kind, string contents, int start, int end)
        {
            this.Kind = Kind;
            this.Contents = contents;
            Start = start;
            End = end;
            Length = end - start;
        }

        public Token(TokenKind Kind, string contents)
        {
            this.Kind = Kind;
            this.Contents = contents;
            Length = contents.Length;
        }

        public static Token EndOfFile
        {
            get { return new Token(TokenKind.EndOfFile, ""); }
        }

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
}