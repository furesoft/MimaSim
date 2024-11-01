namespace MimaSim.MIMA.Parsing.Parsers.High.Passes;

public class EmitPass : IPass
{
    public void Invoke(PassContext context)
    {
        var visitor = new HighParserVisitor();
        context.Tree.Accept(visitor, context.Scope);

        context.Program = visitor.GetRaw();
    }
}