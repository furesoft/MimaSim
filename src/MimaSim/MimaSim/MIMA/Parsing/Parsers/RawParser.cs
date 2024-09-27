using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.AST;
using MimaSim.Core.Parsing.Tokenizer;
using System;
using System.Collections.Generic;

namespace MimaSim.MIMA.Parsing.Parsers;

public class RawParser : IParser
{
    private TokenEnumerator _enumerator;

    public IAstNode Parse(string input)
    {
        var tokenizer = new PrecedenceBasedRegexTokenizer();
        tokenizer.AddDefinition(TokenKind.HexLiteral, "[0-9a-fA-F]{2}");
        tokenizer.AddDefinition(TokenKind.Comment, @"/\\*.*?\\*/", 1);

        var tokens = tokenizer.Tokenize(input);
        _enumerator = new TokenEnumerator(tokens);

        return ParseHexStream();
    }

    private IAstNode ParseHexStream()
    {
        var _nodes = new List<IAstNode>();
        Token token;
        do
        {
            token = _enumerator.Read();

            if (token.Kind == TokenKind.HexLiteral)
            {
                _nodes.Add(NodeFactory.Literal(Convert.ToByte(token.Contents, 16)));
            }
            else if (token.Kind == TokenKind.EndOfFile)
            {
                break;
            }
        } while (token.Kind != TokenKind.EndOfFile);

        return NodeFactory.Call(AstCallNodeType.Group, _nodes.ToArray());
    }
}