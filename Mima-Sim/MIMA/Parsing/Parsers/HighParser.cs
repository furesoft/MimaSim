using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Parsing.Parsers
{
    public class HighParser : IParser
    {
        public DiagnosticBag Diagnostics = new();

        private PrecedenceMap _binaryOperatorPrecedence = new()
        {
            [TokenKind.Plus] = 4,
            [TokenKind.Minus] = 4,
            [TokenKind.Star] = 5,
            [TokenKind.Slash] = 5,

            [TokenKind.GreaterThen] = 3,
            [TokenKind.LessThen] = 3,
            [TokenKind.EqualsEquals] = 3,
            [TokenKind.NotEquals] = 3,
            [TokenKind.GreaterEquals] = 3,
            [TokenKind.LessEquals] = 3,
            [TokenKind.Pipe] = 3,

            [TokenKind.Ampersand] = 1,
            [TokenKind.Hat] = 1,
        };

        private TokenEnumerator _enumerator;

        private PrecedenceMap _unaryOperatorPrecedence = new()
        {
            [TokenKind.Plus] = 6,
            [TokenKind.Minus] = 6,

            [TokenKind.Bang] = 6,
        };

        public IAstNode Parse(string input)
        {
            var tokenizer = new PrecedenceBasedRegexTokenizer();

            tokenizer.AddDefinition(TokenKind.Register, GetRegisterPattern(), 3);

            tokenizer.AddDefinition(TokenKind.HexLiteral, "0x[0-9a-fA-F]{1,6}", 3);
            tokenizer.AddDefinition(TokenKind.IntLiteral, "[0-9]+", 3);
            tokenizer.AddDefinition(TokenKind.Identifier, "[a-zA-Z_][0-9a-zA-F_]*", 4);
            tokenizer.AddDefinition(TokenKind.Plus, @"\+", 4);
            tokenizer.AddDefinition(TokenKind.Minus, @"-", 4);
            tokenizer.AddDefinition(TokenKind.Star, @"\*", 4);
            tokenizer.AddDefinition(TokenKind.Slash, @"/", 4);

            tokenizer.AddDefinition(TokenKind.EqualsToken, @"=", 4);

            tokenizer.AddDefinition(TokenKind.Hat, @"\^", 4);
            tokenizer.AddDefinition(TokenKind.Pipe, @"\|", 4);

            tokenizer.AddDefinition(TokenKind.Ampersand, @"\&", 4);

            tokenizer.AddDefinition(TokenKind.EqualsEquals, @"==", 4);
            tokenizer.AddDefinition(TokenKind.GreaterEquals, @">=", 4);
            tokenizer.AddDefinition(TokenKind.LessEquals, @"<=", 4);

            tokenizer.AddDefinition(TokenKind.NotEquals, @"!=", 4);

            tokenizer.AddDefinition(TokenKind.LessThen, @"<", 4);
            tokenizer.AddDefinition(TokenKind.GreaterThen, @">", 4);

            tokenizer.AddDefinition(TokenKind.Bang, @"\!", 4);

            tokenizer.AddDefinition(TokenKind.TrueKeyword, @"true", 2);
            tokenizer.AddDefinition(TokenKind.FalseKeyword, @"false", 2);

            tokenizer.AddDefinition(TokenKind.IfKeyword, @"if", 2);
            tokenizer.AddDefinition(TokenKind.RegisterKeyword, @"register", 2);
            tokenizer.AddDefinition(TokenKind.VarKeyword, @"var", 2);

            tokenizer.AddDefinition(TokenKind.OpenParen, @"\(", 4);
            tokenizer.AddDefinition(TokenKind.CloseParen, @"\)", 4);

            tokenizer.AddDefinition(TokenKind.OpenBracket, @"\{", 4);
            tokenizer.AddDefinition(TokenKind.CloseBracket, @"\}", 4);

            tokenizer.AddDefinition(TokenKind.Comment, @"/\\*.*?\\*/", 1);

            var tokens = tokenizer.Tokenize(input);
            var enumerator = new TokenEnumerator(tokens);

            _enumerator = enumerator;

            return ParseStatements();
        }

        private string GetRegisterPattern()
        {
            var names = Enum.GetNames(typeof(Registers));
            var namesLowered = names.Select(_ => _.ToLower());

            var allNames = names.Concat(namesLowered);

            return string.Join("|", allNames);
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

        private IAstNode ParseExpression()
        {
            return NodeFactory.Call("BinaryExpression", null, ParseBinaryExpression());
        }

        private IAstNode ParseHexLiteral()
        {
            return NodeFactory.Literal(Convert.ToUInt16(_enumerator.Current.Contents, 16));
        }

        private IAstNode ParseIdentifier()
        {
            var idToken = _enumerator.Read(TokenKind.Identifier);

            return NodeFactory.Id(idToken.Contents);
        }

        private IAstNode ParseIfStatement()
        {
            var keyword = _enumerator.Read(TokenKind.IfKeyword);
            _enumerator.Read(TokenKind.OpenParen);

            var condition = ParseExpression();

            _enumerator.Read(TokenKind.CloseParen);
            _enumerator.Read(TokenKind.OpenBracket);

            var body = ParseStatements();

            _enumerator.Read(TokenKind.CloseBracket);

            return NodeFactory.Call("if", null, condition, body);
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

                case TokenKind.RegisterKeyword:
                    return ParseRegisterExpression();

                case TokenKind.Identifier:
                    return ParseIdentifier();
            }

            Diagnostics.ReportUnknownError();
            return null;
        }

        private IAstNode ParseRegister()
        {
            var regToken = _enumerator.Read();

            return NodeFactory.Literal(Enum.Parse<Registers>(regToken.Contents, true));
        }

        private IAstNode ParseRegisterDefinition()
        {
            _enumerator.Read();

            var register = ParseRegister();

            _enumerator.Read(TokenKind.EqualsToken);

            var value = ParseBinaryExpression();

            return NodeFactory.Call("registerDefinition", null, register, value);
        }

        private IAstNode ParseRegisterExpression()
        {
            var registerKeywordToken = _enumerator.Read();
            var register = ParseRegister();

            return NodeFactory.Call("registerExpression", null, register);
        }

        private IAstNode ParseStatement()
        {
            var lookahead = _enumerator.Current;
            switch (lookahead.Kind)
            {
                case TokenKind.IfKeyword:
                    return ParseIfStatement();

                case TokenKind.RegisterKeyword:
                    return ParseRegisterDefinition();

                case TokenKind.VarKeyword:
                    return ParseVariableDefinition();

                case TokenKind.Identifier:
                    return ParseVariableAssignment();

                default:
                    return ParseExpression();
            }

            return null;
        }

        private IAstNode ParseStatements()
        {
            var _nodes = new List<IAstNode>();
            Token token;
            do
            {
                token = _enumerator.Peek();

                if (token.Kind == TokenKind.EndOfFile || token.Kind == TokenKind.CloseBracket)
                {
                    break;
                }
                else
                {
                    _nodes.Add(ParseStatement());
                }
            } while (token.Kind != TokenKind.EndOfFile);

            return NodeFactory.Call("{}", AstCallNodeType.Group, _nodes.ToArray());
        }

        private IAstNode ParseVariableAssignment()
        {
            var nameToken = _enumerator.Read(TokenKind.Identifier);

            _enumerator.Read(TokenKind.EqualsToken);

            var value = ParseBinaryExpression();

            return NodeFactory.Call("varAssignment", null, NodeFactory.Id(nameToken.Contents), value);
        }

        private IAstNode ParseVariableDefinition()
        {
            _enumerator.Read(TokenKind.VarKeyword);

            var id = ParseIdentifier();

            _enumerator.Read(TokenKind.EqualsToken);

            var value = ParseBinaryExpression();

            return NodeFactory.Call("varDefinition", null, id, value);
        }
    }
}