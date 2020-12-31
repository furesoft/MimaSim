using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Parsing.Tokenizer
{
    public sealed class TokenEnumerator
    {
        public TokenEnumerator(IEnumerable<Token> tokenStream)
        {
            _tokens = tokenStream.ToArray();
            pos = -1;
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

        /// <summary>
        /// Advances through the source document by adding
        /// the given offset to the position index.
        /// </summary>
        private void Advance(int Offset)
        {
            pos += Offset;
        }

        /// <summary>
        /// Reads a single token from the source document,
        /// without updating the current position in the
        /// source document.
        /// </summary>
        public Token Peek(int offset = 0)
        {
            pos++;
            return _tokens[pos + offset];
        }

        /// <summary>
        /// Reads a single token from the source document.
        /// The current position in the source document
        /// is updated.
        /// </summary>
        public Token Read()
        {
            var result = Peek();
            Advance(1);
            return result;
        }

        /// <summary>
        /// Reads a single token from the source document,
        /// and updates the current position. The token's
        /// kind must match the given token kind.
        /// </summary>
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