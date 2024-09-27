using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Visitors;
using System;

namespace MimaSim.MIMA.Parsing.SourceTranslators;

public class HighSourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, ref DiagnosticBag diagnostics)
    {
        var parser = new HighParser();
        var ast = parser.Parse(input);

        if (parser.Diagnostics.IsEmpty)
        {
            var visitor = new HighParserVisitor();

            ast.Visit(visitor);

            return visitor.GetRaw();
        }
        else
        {
            DialogService.OpenError(string.Join('\n', parser.Diagnostics.GetAll()));
            diagnostics = parser.Diagnostics;

            return [];
        }
    }
}