using Silverfly;
using Silverfly.Lexing.IgnoreMatcher.Comments;
using Silverfly.Parselets.Literals;

namespace MimaSim.MIMA.Parsing.Parsers.Raw;


public class RawParser : Parser
{
    protected override void InitLexer(LexerConfig lexer)
    {
        lexer.IgnoreWhitespace();

        lexer.Ignore(new MultiLineCommentIgnoreMatcher("/*", "*/"));
        lexer.Ignore(new SingleLineCommentIgnoreMatcher("//"));

        lexer.MatchPattern(PredefinedSymbols.Number, "[0-9a-fA-F]+");
    }

    protected override void InitParser(ParserDefinition def)
    {
        def.Block(PredefinedSymbols.SOF, PredefinedSymbols.EOF);

        def.Register(PredefinedSymbols.Number, new NumberParselet());
    }
}