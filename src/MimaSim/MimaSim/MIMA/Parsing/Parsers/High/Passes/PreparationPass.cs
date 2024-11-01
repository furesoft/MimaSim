namespace MimaSim.MIMA.Parsing.Parsers.High.Passes;

public class PreparationPass : IPass
{
    public void Invoke(PassContext context)
    {
        var preparationVisitor = new PreparationVisitor();
        context.Tree.Accept(preparationVisitor);

        context.Scope = preparationVisitor.SymbolMap;
    }
}