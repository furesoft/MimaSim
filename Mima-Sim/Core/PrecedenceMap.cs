using MimaSim.Core.Tokenizer;
using System.Collections.Generic;

namespace MimaSim.Core
{
    public class PrecedenceMap : Dictionary<TokenKind, int>
    {
        public int GetPrecedence(TokenKind opKind)
        {
            if (this.ContainsKey(opKind))
            {
                return this[opKind];
            }

            return 0;
        }
    }
}