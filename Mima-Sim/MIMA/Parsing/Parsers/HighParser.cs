using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.Tokenizer;
using System;
using System.Collections.Generic;

namespace MimaSim.MIMA.Parsing.Parsers
{
    public class HighParser : IParser
    {
        public DiagnosticBag Diagnostics = new DiagnosticBag();

        private PrecedenceMap _binaryOperatorPrecedence = new PrecedenceMap
        {
            [TokenKind.Plus] = 4,
            [TokenKind.Minus] = 4,
            [TokenKind.Star] = 5,
            [TokenKind.Slash] = 5,
        };

        private TokenEnumerator _enumerator;

        private PrecedenceMap _unaryOperatorPrecedence = new PrecedenceMap
        {
            [TokenKind.Plus] = 6,
            [TokenKind.Minus] = 6,
        };

        public IAstNode Parse(string input)
        {
            var tokenizer = new PrecedenceBasedRegexTokenizer();

            tokenizer.AddDefinition(TokenKind.HexLiteral, "0x[0-9a-fA-F]{1,6}", 3);
            tokenizer.AddDefinition(TokenKind.IntLiteral, "[0-9]+", 3);
            tokenizer.AddDefinition(TokenKind.Identifier, "[a-zA-Z_][0-9a-zA-F_]*", 4);
            tokenizer.AddDefinition(TokenKind.Plus, @"\+", 4);
            tokenizer.AddDefinition(TokenKind.Minus, @"-", 4);
            tokenizer.AddDefinition(TokenKind.Star, @"\*", 4);
            tokenizer.AddDefinition(TokenKind.Slash, @"/", 4);

            tokenizer.AddDefinition(TokenKind.TrueKeyword, @"true", 2);
            tokenizer.AddDefinition(TokenKind.FalseKeyword, @"false", 2);

            tokenizer.AddDefinition(TokenKind.OpenParen, @"\(", 4);
            tokenizer.AddDefinition(TokenKind.CloseParen, @"\)", 4);

            tokenizer.AddDefinition(TokenKind.Comment, @"/\\*.*?\\*/", 1);

            var tokens = tokenizer.Tokenize(input);
            var enumerator = new TokenEnumerator(tokens);

            _enumerator = enumerator;

            return ParseBinaryExpression();
        }

        private IAstNode ParseBinaryExpression(int parentPrecedence = 0)
        {
            IAstNode left;
            var unaryOperatorPrecedence = _unaryOperatorPrecedence.GetPrecedence(_enumerator.Current.Kind);
            if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
            {
                var operatorToken = _enumerator.Read();
                var operand = ParseBinaryExpression(unaryOperatorPrecedence);

                left = NodeFactory.Call(operatorToken.Contents, null, operand);
            }
            else
            {
                left = ParsePrimaryExpression();
                _enumerator.Read();
            }

            while (true)
            {
                var precedence = _binaryOperatorPrecedence.GetPrecedence(_enumerator.Current.Kind);

                if (precedence == 0 || precedence <= parentPrecedence)
                    break;

                var operatorToken = _enumerator.Read();
                var right = ParseBinaryExpression(precedence);

                left = NodeFactory.Call(operatorToken.Contents, null, left, right);
            }

            return left;
        }

        private IAstNode ParseBooleanLiteral()
        {
            var current = _enumerator.Peek();

            var isTrue = current.Kind == TokenKind.TrueKeyword;
            var value = isTrue ? 1 : 0;

            return NodeFactory.Literal((ushort)value);
        }

        private IAstNode ParseHexLiteral()
        {
            return NodeFactory.Literal(Convert.ToUInt16(_enumerator.Current.Contents, 16));
        }

        private IAstNode ParseIntLiteral()
        {
            return NodeFactory.Literal(ushort.Parse(_enumerator.Current.Contents));
        }

        private IAstNode ParseParenthesizedExpression()
        {
            var left = _enumerator.Read(TokenKind.OpenParen);
            var expression = ParseBinaryExpression();
            var right = _enumerator.Read(TokenKind.CloseParen);

            return expression;
        }

        private IAstNode ParsePrimaryExpression()
        {
            var current = _enumerator.Current;

            switch (current.Kind)
            {
                case TokenKind.OpenParen:
                    return ParseParenthesizedExpression();

                case TokenKind.FalseKeyword:
                case TokenKind.TrueKeyword:
                    return ParseBooleanLiteral();

                case TokenKind.HexLiteral:
                    return ParseHexLiteral();

                case TokenKind.IntLiteral:
                    return ParseIntLiteral();
            }

            return null;
        }
    }
}