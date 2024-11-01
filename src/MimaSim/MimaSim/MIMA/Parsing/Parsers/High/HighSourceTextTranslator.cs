using System;
using MimaSim.Core.Parsing;
using MimaSim.MIMA.Parsing.Parsers.High.Passes;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly.Text;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class HighSourceTextTranslator : ISourceTextTranslator
{
    public byte[] ToRaw(string input, out SourceDocument document)
    {
        var passManager = new PassManager();

        passManager.AddPass<ParsingPass>();
        passManager.AddPass<PreparationPass>();
        passManager.AddPass<ValidationPass>();
        passManager.AddPass<EmitPass>();

        var context = new PassContext();
        passManager.Run(context);

        document = context.Document;

        return context.Program;
    }
}