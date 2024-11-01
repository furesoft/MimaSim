namespace MimaSim.MIMA.Parsing.Parsers.High.Passes;

public class ParsingPass : IPass
{
    public void Invoke(PassContext context)
    {
        var parser = new HighParser();
        var ast = parser.Parse(context.Input);

        context.Tree = ast.Tree;
        context.Document = ast.Document;
    }
}