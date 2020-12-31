using MimaSim.MIMA.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Parsing.Tokenizer
{
    public class TokenMatch
    {
        public TokenKind TokenType { get; set; }
        public string Value { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Precedence { get; set; }
    }
}