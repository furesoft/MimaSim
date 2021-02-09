using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core.Tokenizer
{
    public sealed class TokenEnumerator
    {
        public TokenEnumerator(IEnumerable<Token> tokenStream)
        {
            _tokens = tokenStream.ToArray();
            pos = 0;
        }

        private Token[] _tokens;
        private int pos;

        public int BackTrackPos()
        {
            return pos;
        }

        public void BackTrack(int pos)
        {
            this.pos = pos;
        }

        private void Advance(int Offset)
        {
            pos += Offset;
        }

        public Token Peek(int offset = 0)
        {
            return _tokens[pos + offset];
        }

        public Token Read()
        {
            var result = Peek();

            if (result.Kind == TokenKind.Comment)
            {
                Advance(1);
                result = Peek();
            }

            Advance(1);
            return result;
        }

        public Token Read(TokenKind Kind)
        {
            var result = Read();
            if (result.Kind != Kind)
                throw new Exception(
                    "Expected a token of type '" + Kind +
                    "', got '" + result.Contents + "'.");

            return result;
        }
    }
}