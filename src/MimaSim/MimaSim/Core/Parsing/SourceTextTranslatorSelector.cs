using MimaSim.MIMA.Parsing.Parsers.Assembler;
using MimaSim.MIMA.Parsing.Parsers.High;
using MimaSim.MIMA.Parsing.Parsers.Raw;

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