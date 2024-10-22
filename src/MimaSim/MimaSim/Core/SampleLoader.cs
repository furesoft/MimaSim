using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MimaSim.Core;

public class SampleLoader
{
    private const string BaseResourcePath = "MimaSim.Resources.samples.";
    private readonly Dictionary<(LanguageName lang, string name), string> _samples = new();

    public void Register(LanguageName language, string name, string src)
    {
        _samples.TryAdd((language, name), src);
    }

    public void FromResources(Assembly assembly)
    {
        foreach (LanguageName language in Enum.GetValues(typeof(LanguageName)))
        {
            var languagePath = $"{BaseResourcePath}{language.ToString().ToLower()}.";
            var resourceNames = assembly.GetManifestResourceNames();

            foreach (var resourceName in resourceNames)
                if (resourceName.StartsWith(languagePath) && resourceName.EndsWith(".sample"))
                {
                    var sampleName = Path.GetFileNameWithoutExtension(resourceName).Split('.').Last();

                    using var stream = assembly.GetManifestResourceStream(resourceName);
                    using var reader = new StreamReader(stream!);
                    var sampleContent = reader.ReadToEnd();

                    Register(language, sampleName, sampleContent);
                }
        }
    }

    public string? GetSample(LanguageName language, string? name)
    {
        return name is null ? null : _samples.GetValueOrDefault((language, name));
    }

    public IEnumerable<string> GetSampleNamesFor(LanguageName language)
    {
        return _samples.Keys.Where(_ => _.lang == language).Select(_ => _.name);
    }
}