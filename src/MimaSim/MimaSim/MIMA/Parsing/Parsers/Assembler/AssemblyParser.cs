using MimaSim.MIMA.Parsing.Parsers.Assembler.Parselets;
using Silverfly;
using Silverfly.Lexing.IgnoreMatcher.Comments;
using Silverfly.Lexing.Matcher;
using Silverfly.Parselets;
using Silverfly.Parselets.Literals;
using static Silverfly.PredefinedSymbols;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class AssemblyParser : Parser
{
    protected override void InitLexer(LexerConfig lexer)
    {
        lexer.IgnoreWhitespace();

        lexer.Ignore(new MultiLineCommentIgnoreMatcher("/*", "*/"));
        lexer.Ignore(new SingleLineCommentIgnoreMatcher("//"));

        lexer.MatchNumber(true, false);
        lexer.AddMatcher(new EnumMatcher<Registers>("#register"));
        lexer.AddMatcher(new EnumMatcher<Mnemnonics>("#mnemnonic"));
    }

    protected override void InitParser(ParserDefinition def)
    {
        def.Block(SOF, EOF);

        def.Register(Number, new NumberParselet());
        def.Register(Name, new NameParselet());
        def.Register("#register", new EnumParselet<Registers>());
        def.Register("#mnemnonic", new InstructionParselet());

        def.Prefix("&", tag: "address");
        def.Prefix("$", tag: "labelref");
        def.Postfix(":", tag: "label");
    }
}