using MimaSim.MIMA.Parsing.SourceTranslators;

namespace MimaSim.Core
{
    public static class SourceTextTranslatorSelector
    {
        public static ISourceTextTranslator Select(LanguageName language)
        {
            switch (language)
            {
                case LanguageName.Maschinencode:
                    return new RawSourceTextTranslator();

                case LanguageName.Assembly:
                    return new AssemblySourceTextTranslator();

                case LanguageName.Hochsprache:
                    break;

                default:
                    return new RawSourceTextTranslator();
            }

            return null;
        }
    }
}