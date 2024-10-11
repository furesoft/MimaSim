using System;
using MimaSim.Core.Parsing.AST;
using MimaSim.Core.Parsing.Tokenizer;
using System.Linq;
using Silverfly;
using Silverfly.Lexing.IgnoreMatcher.Comments;
using Silverfly.Nodes;
using Silverfly.Parselets.Literals;

namespace MimaSim.MIMA.Parsing.Parsers;


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