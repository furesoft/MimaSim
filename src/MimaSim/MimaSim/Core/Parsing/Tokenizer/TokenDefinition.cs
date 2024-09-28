using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MimaSim.Core.Parsing.Tokenizer;

public class TokenDefinition(TokenKind returnsToken, string regexPattern, int precedence)
{
    private readonly Regex _regex = new(regexPattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public IEnumerable<TokenMatch> FindMatches(string inputString)
    {
        var matches = _regex.Matches(inputString);
        for (int i = 0; i < matches.Count; i++)
        {
            yield return new TokenMatch()
            {
                StartIndex = matches[i].Index,
                EndIndex = matches[i].Index + matches[i].Length,
                TokenType = returnsToken,
                Value = matches[i].Value,
                Precedence = precedence
            };
        }
    }
}