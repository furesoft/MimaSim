namespace MimaSim.Core.Parsing;

public interface ISourceTextTranslator
{
    byte[] ToRaw(string input, out Silverfly.Text.SourceDocument document);
}