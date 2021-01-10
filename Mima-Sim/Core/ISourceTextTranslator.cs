namespace MimaSim.Core
{
    public interface ISourceTextTranslator
    {
        byte[] ToRaw(string input);
    }
}