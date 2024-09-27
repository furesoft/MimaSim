namespace MimaSim.Core.Parsing;

public interface ISourceTextTranslator
{
    byte[] ToRaw(string input, ref DiagnosticBag diagnostics);
}