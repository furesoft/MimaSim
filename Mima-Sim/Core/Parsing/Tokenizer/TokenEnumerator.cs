using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core.Parsing.Tokenizer
{
    public sealed class TokenEnumerator
    {
        private readonly Token[] _tokens;
        private int _position;

        public TokenEnumerator(IEnumerable<Token> tokenStream)
        {
            _tokens = tokenStream.ToArray();
            _position = 0;
        }

        public Token Current
        {
            get
            {
                if (_position < _tokens.Length)
                {
                    return _tokens[_position];
                }

                return Token.EndOfFile;
            }
        }

        public void BackTrack(int pos)
        {
            this._position = pos;
        }

        public int BackTrackPos()
        {
            return _position;
        }

        public Token Peek(int offset = 0)
        {
            if ((_position + offset) <= (_tokens.Length - 1))
            {
                return _tokens[_position + offset];
            }

            return Token.EndOfFile;
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

        private void Advance(int offset)
        {
            _position += offset;
        }
    }
}