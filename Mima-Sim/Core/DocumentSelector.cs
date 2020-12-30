using MimaSim.Properties;

namespace MimaSim.Core
{
    public static class DocumentSelector
    {
        public static string GetDocument(string name)
        {
            switch (name)
            {
                case "Maschinencode":
                    return Resources.Maschinencode;

                case "Assemblycode":
                    return Resources.Assemblycode;

                case "Hochsprache":
                    return Resources.Hochsprache;

                default:
                    return $"<Fehler> Dokument '{name}' nicht gefunden!";
            }
        }
    }
}