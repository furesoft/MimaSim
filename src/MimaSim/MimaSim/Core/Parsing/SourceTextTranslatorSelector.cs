using MimaSim.MIMA.Parsing.SourceTranslators;

namespace MimaSim.Core.Parsing;

public static class SourceTextTranslatorSelector
{
    public static ISourceTextTranslator Select(LanguageName language)
    {
        return language switch
        {
            LanguageName.Maschinencode => new RawSourceTextTranslator(),
            LanguageName.Assembly => new AssemblySourceTextTranslator(),
            LanguageName.Hochsprache => new HighSourceTextTranslator(),
            _ => new RawSourceTextTranslator()
        };
    }
}