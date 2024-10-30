using System;
using MimaSim.MIMA.Parsing.Parsers.High.Parselets;
using Silverfly;
using Silverfly.Parselets;
using Silverfly.Parselets.Literals;
using static Silverfly.PredefinedSymbols;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighParser : Parser
{
    protected override void InitLexer(LexerConfig lexer)
    {
        lexer.IgnoreWhitespace();

        lexer.MatchBoolean();
        lexer.MatchString("\"", "\"");
        lexer.MatchNumber(true, true);
        lexer.AddKeywords("asm", "func"); // asm "mov x, y"

        lexer.AddSymbols(")", "{", "}");

    }

    protected override void InitParser(ParserDefinition def)
    {
        def.Register(Number, new NumberParselet());
        def.Register(PredefinedSymbols.Boolean, new BooleanLiteralParselet());
        def.Register(Name, new NameParselet());
        def.Register("asm", new InlineAsmParselet());
        def.Register("func", new FuncDefParselet());
        def.Register("(", new CallParselet(0));
    }
}