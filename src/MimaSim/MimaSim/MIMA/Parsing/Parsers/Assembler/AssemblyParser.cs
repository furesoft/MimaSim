using MimaSim.MIMA.Parsing.Parsers.Assembler.Parselets;
using MimaSim.MIMA.Parsing.Parsers.High.Parselets;
using Silverfly;
using Silverfly.Lexing.IgnoreMatcher.Comments;
using Silverfly.Lexing.NameAdvancers;
using Silverfly.Parselets.Literals;
using static Silverfly.PredefinedSymbols;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class AssemblyParser : Parser
{
    protected override void InitLexer(LexerConfig lexer)
    {
        lexer.UseNameAdvancer(new CStyleNameAdvancer());
        lexer.Ignore(" ", "\t", "\r");

        lexer.Ignore(new MultiLineCommentIgnoreMatcher("/*", "*/"));
        lexer.Ignore(new SingleLineCommentIgnoreMatcher("#"));

        lexer.MatchNumber(true, false);
        lexer.MatchString("'", "'", allowEscapeChars: false, allowUnicodeChars: false);

        lexer.AddSymbols("(", ")", ",", "{", "}", "/*", "*/", ":");
        lexer.AddMatcher(new NewLineMatcher());
    }

    protected override void InitParser(ParserDefinition def)
    {
        def.Block(SOF, EOF);

        def.Register(Number, new NumberParselet());
        def.Register(Name, new InstructionParselet());
        def.Register("macro", new MacroParselet());
        def.Register(String, new StringLiteralParselet());

        def.Prefix("&", tag: "address");
        def.Register("$", new LabelRefParselet());
        def.Postfix(":", tag: "label");
    }
}