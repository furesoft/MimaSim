using System.Collections.Generic;
using System.Linq;
using MimaSim.Core;

namespace MimaSim.Samples;

public class SampleLoader
{
    private Dictionary<(LanguageName lang, string name), string> _samples = new();
    public void Register(LanguageName language, string name, string src)
    {
        _samples.TryAdd((language, name), src);
    }

    public string GetSample(LanguageName language, string name)
    {
        return _samples[(language, name)];
    }

    public IEnumerable<string> GetSampleNamesFor(LanguageName language)
    {
        return _samples.Keys.Where(_ => _.lang == language).Select(_ => _.name);
    }
}