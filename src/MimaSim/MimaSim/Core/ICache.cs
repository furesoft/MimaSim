namespace MimaSim.Core;

public interface ICache
{
    void AddOrUpdate(string key, object value);
    T? Get<T>(string key);
}