using MimaSim.MIMA.Parsing.Parsers.High.AST;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class PreparationVisitor : NodeVisitor
{
    public readonly Scope SymbolMap =  new(true);
    public PreparationVisitor()
    {
        For<FuncDefNode>(VisitFuncDef);
    }

    private void VisitFuncDef(FuncDefNode def)
    {
        SymbolMap.Define(def.Name, SymbolType.Function);
    }
}