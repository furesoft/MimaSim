using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly.Text;

namespace MimaSim.MIMA.Parsing.Parsers.High.Passes;

public class ValidationPass : IPass
{
    public void Invoke(PassContext context)
    {
        if (context.Scope.Get("main") is not FunctionSymbol)
        {
            context.Document.AddMessage(MessageSeverity.Error, "Main function not found", SourceRange.Empty);
        }
    }
}