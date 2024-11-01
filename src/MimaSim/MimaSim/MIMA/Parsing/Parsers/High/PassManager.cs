using System.Collections.Generic;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class PassManager
{
    private readonly List<IPass> _passes = [];
    public void AddPass<T>()
        where T : class, IPass, new()
    {
        _passes.Add(new T());
    }

    public void Run(PassContext context)
    {
        foreach (var pass in _passes)
        {
            pass.Invoke(context);
        }
    }
}