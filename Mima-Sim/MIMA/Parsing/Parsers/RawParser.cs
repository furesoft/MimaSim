using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.Tokenizer;
using System;
using System.Collections.Generic;

namespace MimaSim.MIMA.Parsing.Parsers
{
    public class RawParser : IParser
    {
        public IAstNode Parse(string input)
        {
            var tokenizer = new PrecedenceBasedRegexTokenizer();
            tokenizer.AddDefinition(TokenKind.HexLiteral, "[0-9a-dA-D]{2}");

            var tokens = tokenizer.Tokenize(input);
            var enumerator = new TokenEnumerator(tokens);

            return ParseHexStream(enumerator);
        }

        private IAstNode ParseHexStream(TokenEnumerator enumerator)
        {
            var _nodes = new List<IAstNode>();
            Token token;
            do
            {
                token = enumerator.Read();

                if (token.Kind == TokenKind.HexLiteral)
                {
                    _nodes.Add(NodeFactory.Literal(Convert.ToByte(token.Contents, 16)));
                }
            } while (token.Kind != TokenKind.EndOfFile);

            return NodeFactory.Call("{}", _nodes.ToArray());
        }
    }
}