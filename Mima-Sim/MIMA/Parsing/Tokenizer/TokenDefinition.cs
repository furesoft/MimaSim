using MimaSim.MIMA.Parsing.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MimaSim.MIMA.Tokenizer
{
    public class TokenDefinition
    {
        private Regex _regex;
        private readonly TokenKind _returnsToken;
        private readonly int _precedence;

        public TokenDefinition(TokenKind returnsToken, string regexPattern, int precedence)
        {
            _regex = new Regex(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            _returnsToken = returnsToken;
            _precedence = precedence;
        }

        public IEnumerable<TokenMatch> FindMatches(string inputString)
        {
            var matches = _regex.Matches(inputString);
            for (int i = 0; i < matches.Count; i++)
            {
                yield return new TokenMatch()
                {
                    StartIndex = matches[i].Index,
                    EndIndex = matches[i].Index + matches[i].Length,
                    TokenType = _returnsToken,
                    Value = matches[i].Value,
                    Precedence = _precedence
                };
            }
        }
    }
}