using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.Tokenizer;

namespace MimaSim.MIMA.Parsing.Parsers
{
    public class RawParser : IParser
    {
        public IAstNode Parse(string input)
        {
            var tokenizer = new PrecedenceBasedRegexTokenizer();

            var tokens = tokenizer.Tokenize(input);
            var enumerator = new TokenEnumerator(tokens);

            return ParseHexStream(enumerator);
        }

        private IAstNode ParseHexStream(TokenEnumerator enumerator)
        {
            Token token;
            do
            {
                token = enumerator.Read(TokenKind.HexLiteral);
            } while (token.Kind != TokenKind.EndOfFile);

            return null;
        }
    }
}