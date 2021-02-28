namespace MimaSim.Core
{
    public interface IUITab
    {
        int Index { get; }
        string Title { get; }

        object ViewModel { get; }
    }
}