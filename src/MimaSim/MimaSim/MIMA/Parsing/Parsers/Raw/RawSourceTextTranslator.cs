﻿using MimaSim.Core;
using MimaSim.Core.Parsing;

namespace MimaSim.MIMA.Parsing.Parsers.Raw;

public class RawSourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, ref DiagnosticBag diagnostics)
    {
        var parser = new RawParser();
        var ast = parser.Parse(input);

        if (ast.Document.Messages.Count > 0)
        {
            diagnostics.ReportUnknownError();
            return [];
        }

        var visitor = new RawParserVisitor();

        ast.Tree.Accept(visitor);

        return visitor.GetRaw();
    }
}