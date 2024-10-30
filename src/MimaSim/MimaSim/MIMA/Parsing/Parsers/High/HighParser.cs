using System;
using MimaSim.MIMA.Parsing.Parsers.High.Parselets;
using Silverfly;
using Silverfly.Parselets;
using Silverfly.Parselets.Literals;
using static Silverfly.PredefinedSymbols;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighParser : Parser
{
/*
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


        var tokens = tokenizer.Tokenize(input);
        var enumerator = new TokenEnumerator(tokens);

        _enumerator = enumerator;

        return ParseStatements();
    }
*/
    protected override void InitLexer(LexerConfig lexer)
    {
        lexer.IgnoreWhitespace();

        lexer.MatchBoolean();
        lexer.MatchString("\"", "\"");
        lexer.MatchNumber(true, true);
        lexer.AddKeywords("asm"); // asm "mov x, y"
    }

    protected override void InitParser(ParserDefinition def)
    {
        def.Register(Number, new NumberParselet());
        def.Register(Name, new NameParselet());
        def.Register("asm", new InlineAsmParselet());
    }
}