using MimaSim.MIMA.Parsing.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Tokenizer
{
    /// <summary>
    /// Defines a type for tokens.
    /// </summary>
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

        public int Length { get; set; }

        /// <summary>
        /// Gets this token's kind.
        /// </summary>
        public TokenKind Kind { get; private set; }

        /// <summary>
        /// Gets this token's contents: the substring
        /// of the source document that this token
        /// represents.
        /// </summary>
        public string Contents { get; private set; }

        public override string ToString()
        {
            return "(" + Kind + ", \"" + Contents + "\")";
        }

        /// <summary>
        /// Gets a token that represents the end-of-file
        /// marker.
        /// </summary>
        public static Token EndOfFile
        {
            get { return new Token(TokenKind.EndOfFile, ""); }
        }

        /// <summary>
        /// Tests if the given token kind is used for trivia
        /// tokens: tokens that are important to the lexing
        /// process, but can be safely ignored afterward.
        /// Whitespace and comments are trivia.
        /// </summary>
        public static bool IsTrivia(TokenKind Kind)
        {
            return Kind == TokenKind.Whitespace
                || Kind == TokenKind.Comment;
        }
    }
}