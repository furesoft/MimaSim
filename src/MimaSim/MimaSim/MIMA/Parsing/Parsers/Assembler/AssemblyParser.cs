using MimaSim.MIMA.Parsing.Parsers.Assembler.Parselets;
using Silverfly;
using Silverfly.Lexing.IgnoreMatcher.Comments;
using Silverfly.Parselets.Literals;
using static Silverfly.PredefinedSymbols;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class AssemblyParser : Parser
{
    protected override void InitLexer(LexerConfig lexer)
    {
        lexer.Ignore(" ", "\t", "\r");

        lexer.Ignore(new MultiLineCommentIgnoreMatcher("/*", "*/"));
        lexer.Ignore(new SingleLineCommentIgnoreMatcher("#"));

        lexer.MatchNumber(true, false);
        lexer.MatchString("'", "'", allowEscapeChars: false, allowUnicodeChars: false);

        lexer.AddSymbols("(", ")", ",", "{", "}", "/*", "*/");
        lexer.AddMatcher(new NewLineMatcher());
    }

    protected override void InitParser(ParserDefinition def)
    {
        def.Block(SOF, EOF);

        def.Register(Number, new NumberParselet());
        def.Register(Name, new InstructionParselet());
        def.Register("macro", new MacroParselet());
        def.Register(PredefinedSymbols.String, new StringLiteralParselet());

        def.Prefix("&", tag: "address");
        def.Prefix("$", tag: "labelref");
        def.Postfix(":", tag: "label");
    }
}