using System;
using Silverfly;
using Silverfly.Lexing;

namespace MimaSim.MIMA.Parsing.Parsers.Assembler;

public class NewLineMatcher : IMatcher
{
    public bool Match(Lexer lexer, char c) => c == '\n' && lexer.IsContext<InstructionContext>();

    public Token Build(Lexer lexer, ref int index, ref int column, ref int line)
    {
        while (lexer.IsNotAtEnd() && lexer.Peek() == '\n')
        {
            lexer.Advance();
        }

        return new Token("\n", new ReadOnlyMemory<char>(['\n']), line, column, lexer.Document);
    }
}