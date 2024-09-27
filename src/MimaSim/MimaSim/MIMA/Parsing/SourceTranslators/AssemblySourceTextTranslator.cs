using MimaSim.Controls;
using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Visitors;
using System;

namespace MimaSim.MIMA.Parsing.SourceTranslators;

public class AssemblySourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, ref DiagnosticBag diagnostics)
    {
        var parser = new AssemblyParser();
        var ast = (CallNode)parser.Parse(input);

        if (ast.IsEmpty || ast.Type == null)
        {
            diagnostics.ReportUnknownError();

            return [];
        }

        if (parser.Diagnostics.IsEmpty)
        {
            var visitor = new AssemblyVisitor();

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