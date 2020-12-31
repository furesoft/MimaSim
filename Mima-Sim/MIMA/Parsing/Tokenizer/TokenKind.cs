using MimaSim.MIMA.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Parsing.Tokenizer
{
    public enum TokenKind
    {
        /// <summary>
        /// The "undefined" token kind. Tokens of the
        /// "undefined" kind were not recognized by the lexer.
        /// </summary>
        Undefined,

        /// <summary>
        /// A special token that marks the end of a file.
        /// </summary>
        EndOfFile,

        /// <summary>
        /// The token kind for whitespace.
        /// </summary>
        Whitespace,

        /// <summary>
        /// The token kind for comments.
        /// </summary>
        Comment,

        /// <summary>
        /// An identifier.
        /// </summary>
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
    }
}