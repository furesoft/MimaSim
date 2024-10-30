using MimaSim.MIMA.Parsing.Parsers.High.AST;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class PreparationVisitor : NodeVisitor
{
    public readonly SymbolMap SymbolMap =  new();
    public PreparationVisitor()
    {
        For<FuncDefNode>(VisitFuncDef);
    }

    private void VisitFuncDef(FuncDefNode def)
    {
        SymbolMap.AddSymbol(def.Name, SymbolType.Function);
    }
}