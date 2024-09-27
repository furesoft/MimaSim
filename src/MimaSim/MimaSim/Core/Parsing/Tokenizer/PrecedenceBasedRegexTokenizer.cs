using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core.Parsing.Tokenizer;

public class PrecedenceBasedRegexTokenizer
{
    private readonly List<TokenDefinition> _tokenDefinitions;

    public PrecedenceBasedRegexTokenizer()
    {
        _tokenDefinitions = [];
    }

    public void AddDefinition(TokenKind kind, string pattern, int precedence = 1)
    {
        _tokenDefinitions.Add(new TokenDefinition(kind, pattern, precedence));
    }

    public IEnumerable<Token> Tokenize(string src)
    {
        var tokenMatches = FindTokenMatches(src);

        var groupedByIndex = tokenMatches.GroupBy(x => x.StartIndex)
            .OrderBy(x => x.Key)
            .ToList();

        TokenMatch lastMatch = null;
        for (int i = 0; i < groupedByIndex.Count; i++)
        {
            var bestMatch = groupedByIndex[i].OrderBy(x => x.Precedence).First();
            if (lastMatch != null && bestMatch.StartIndex < lastMatch.EndIndex)
                continue;

            yield return new Token(bestMatch.TokenType, bestMatch.Value, bestMatch.StartIndex, bestMatch.EndIndex);

            lastMatch = bestMatch;
        }

        yield return new Token(TokenKind.EndOfFile, string.Empty);
    }

    private List<TokenMatch> FindTokenMatches(string src)
    {
        var tokenMatches = new List<TokenMatch>();

        foreach (var tokenDefinition in _tokenDefinitions)
            tokenMatches.AddRange(tokenDefinition.FindMatches(src).ToList());

        return tokenMatches;
    }
}