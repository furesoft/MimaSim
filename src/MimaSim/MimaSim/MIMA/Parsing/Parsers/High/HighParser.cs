using System;
using System.Collections.Generic;
using System.Linq;
using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.AST;
using MimaSim.Core.Parsing.Tokenizer;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighParser : IParser
{
    public DiagnosticBag Diagnostics = new();

    private readonly PrecedenceMap _binaryOperatorPrecedence = new()
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

    private readonly PrecedenceMap _unaryOperatorPrecedence = new()
    {
        [TokenKind.Plus] = 6,
        [TokenKind.Minus] = 6,

        [TokenKind.Bang] = 6,
    };

    private readonly Dictionary<string, AstCallNodeType> OperatorNodeTypeMap = new()
    {
        ["+"] = AstCallNodeType.Addition,
        ["-"] = AstCallNodeType.Subtraktion,
        ["*"] = AstCallNodeType.Multiplication,
        ["/"] = AstCallNodeType.Division,

        ["&"] = AstCallNodeType.And,
        ["!"] = AstCallNodeType.Not,
        ["^"] = AstCallNodeType.Xor,
        ["|"] = AstCallNodeType.Or,

        ["<"] = AstCallNodeType.LessThen,
        ["<="] = AstCallNodeType.LessEqual,
        ["=="] = AstCallNodeType.Equal,
        ["!="] = AstCallNodeType.NotEqual,
        [">="] = AstCallNodeType.GreaterEqual,
        [">"] = AstCallNodeType.GreaterThan,
    };

    private TokenEnumerator _enumerator;

    public IAstNode Parse(string input)
    {
        var tokenizer = new PrecedenceBasedRegexTokenizer();

        tokenizer.AddDefinition(TokenKind.StringLiteral, @"'.*?'", 8);
        tokenizer.AddDefinition(TokenKind.Register, GetRegisterPattern(), 3);

        tokenizer.AddDefinition(TokenKind.HexLiteral, "0x[0-9a-fA-F]{1,6}", 3);
        tokenizer.AddDefinition(TokenKind.IntLiteral, "-?[0-9]+", 3);
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
        tokenizer.AddDefinition(TokenKind.Comma, ",", 4);

        tokenizer.AddDefinition(TokenKind.OpenSquare, @"\[", 4);
        tokenizer.AddDefinition(TokenKind.CloseSquare, @"\]", 4);

        tokenizer.AddDefinition(TokenKind.TrueKeyword, @"true", 2);
        tokenizer.AddDefinition(TokenKind.FalseKeyword, @"false", 2);

        tokenizer.AddDefinition(TokenKind.IfKeyword, @"if", 2);
        tokenizer.AddDefinition(TokenKind.RegisterKeyword, @"register", 2);
        tokenizer.AddDefinition(TokenKind.VarKeyword, @"var", 2);
        tokenizer.AddDefinition(TokenKind.ArrayKeyword, @"array", 2);
        tokenizer.AddDefinition(TokenKind.AddressOfKeyword, @"addressof", 2);
        tokenizer.AddDefinition(TokenKind.LoopKeyword, @"loop", 2);

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

    private IAstNode ParseAddressOfExpression()
    {
        _ = _enumerator.Read();
        var address = ParsePrimaryExpression();

        return NodeFactory.Call(AstCallNodeType.AddressOfExpression, address);
    }

    private IAstNode ParseArrayExpression()
    {
        _ = _enumerator.Read();
        _enumerator.Read(TokenKind.OpenParen);
        IAstNode values = ParseArrayValues();

        _enumerator.Read(TokenKind.CloseParen);

        return NodeFactory.Call(AstCallNodeType.ArrayExpression, values);
    }

    private IAstNode ParseArrayValues()
    {
        List<IAstNode> values = [];

        Token token;
        do
        {
            token = _enumerator.Peek();

            if (token.Kind == TokenKind.IntLiteral)
            {
                values.Add(ParseIntLiteral());
                _enumerator.Read();
            }
            else if (token.Kind == TokenKind.HexLiteral)
            {
                values.Add(ParseHexLiteral());
                _enumerator.Read();
            }

            if (_enumerator.Peek().Kind != TokenKind.CloseParen)
            {
                _enumerator.Read(TokenKind.Comma);
            }
        } while (token.Kind != TokenKind.CloseParen);

        return NodeFactory.Call(AstCallNodeType.Group, values.ToArray());
    }

    private IAstNode ParseBinaryExpression(int parentPrecedence = 0)
    {
        IAstNode left;
        var unaryOperatorPrecedence = _unaryOperatorPrecedence.GetPrecedence(_enumerator.Current.Kind);
        if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
        {
            var operatorToken = _enumerator.Read();
            var operand = ParseBinaryExpression(unaryOperatorPrecedence);

            left = NodeFactory.Call(OperatorNodeTypeMap[operatorToken.Contents], operand);
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

            left = NodeFactory.Call(OperatorNodeTypeMap[operatorToken.Contents], left, right);
        }

        return left;
    }

    private IAstNode ParseBooleanLiteral()
    {
        var current = _enumerator.Peek();

        var isTrue = current.Kind == TokenKind.TrueKeyword;
        var value = isTrue ? 1 : 0;

        return NodeFactory.Literal((short)value);
    }

    private IAstNode ParseExpression()
    {
        return NodeFactory.Call(AstCallNodeType.BinaryExpresson, ParseBinaryExpression());
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
        _ = _enumerator.Read(TokenKind.IfKeyword);
        _enumerator.Read(TokenKind.OpenParen);

        var condition = ParseExpression();

        _enumerator.Read(TokenKind.CloseParen);
        _enumerator.Read(TokenKind.OpenBracket);

        var body = ParseStatements();

        _enumerator.Read(TokenKind.CloseBracket);

        return NodeFactory.Call(AstCallNodeType.IfStatement, condition, body);
    }

    private IAstNode ParseIntLiteral()
    {
        return NodeFactory.Literal(short.Parse(_enumerator.Current.Contents));
    }

    private IAstNode ParseLoopStatement()
    {
        _ = _enumerator.Read(TokenKind.LoopKeyword);
        _enumerator.Read(TokenKind.OpenBracket);

        var body = ParseStatements();

        _enumerator.Read(TokenKind.CloseBracket);

        return NodeFactory.Call(AstCallNodeType.LoopStatement, body);
    }

    private IAstNode ParseNameExpression()
    {
        var idToken = _enumerator.Current;

        return NodeFactory.Id(idToken.Contents);
    }

    private IAstNode ParseParenthesizedExpression()
    {
        _ = _enumerator.Read(TokenKind.OpenParen);
        var expression = ParseBinaryExpression();
        _ = _enumerator.Read(TokenKind.CloseParen);

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

            case TokenKind.StringLiteral:
                return ParseStringLiteral();

            case TokenKind.RegisterKeyword:
                return ParseRegisterExpression();

            case TokenKind.ArrayKeyword:
                return ParseArrayExpression();

            case TokenKind.AddressOfKeyword:
                return ParseAddressOfExpression();

            case TokenKind.Identifier:
                return ParseNameExpression();
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

        return NodeFactory.Call(AstCallNodeType.RegisterDefinitionStatement, register, value);
    }

    private IAstNode ParseRegisterExpression()
    {
        _ = _enumerator.Read();
        var register = ParseRegister();

        return NodeFactory.Call(AstCallNodeType.RegisterExpression, register);
    }

    private IAstNode ParseStatement()
    {
        var lookahead = _enumerator.Current;
        return lookahead.Kind switch
        {
            TokenKind.IfKeyword => ParseIfStatement(),
            TokenKind.RegisterKeyword => ParseRegisterDefinition(),
            TokenKind.VarKeyword => ParseVariableDefinition(),
            TokenKind.Identifier => ParseVariableAssignment(),
            TokenKind.LoopKeyword => ParseLoopStatement(),
            _ => ParseExpression(),
        };
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

        return NodeFactory.Call(AstCallNodeType.Group, _nodes.ToArray());
    }

    private IAstNode ParseStringLiteral()
    {
        var content = _enumerator.Current.Contents;
        content = content[1..^1];

        return NodeFactory.Literal(content);
    }

    private IAstNode ParseVariableAssignment()
    {
        var nameToken = _enumerator.Read(TokenKind.Identifier);

        _enumerator.Read(TokenKind.EqualsToken);

        var value = ParseExpression();

        return NodeFactory.Call(AstCallNodeType.VariableAssignmentStatement, NodeFactory.Id(nameToken.Contents), value);
    }

    private IAstNode ParseVariableDefinition()
    {
        _enumerator.Read(TokenKind.VarKeyword);

        var id = ParseIdentifier();

        _enumerator.Read(TokenKind.EqualsToken);

        var value = ParseExpression();

        return NodeFactory.Call(AstCallNodeType.VariableDefinitionStatement, id, value);
    }
}