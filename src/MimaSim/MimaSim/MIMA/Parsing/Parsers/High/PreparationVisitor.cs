using MimaSim.MIMA.Parsing.Parsers.High.AST;
using MimaSim.MIMA.Parsing.Parsers.High.Symbols;
using Silverfly;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class PreparationVisitor : NodeVisitor
{
    public readonly Scope SymbolMap =  new(true);
    public PreparationVisitor()
    {
        For<BlockNode>(VisitBlock);
        For<FuncDefNode>(VisitFuncDef);
    }

    private void VisitFuncDef(FuncDefNode def)
    {
        SymbolMap.Define(def.Name, SymbolType.Function);
    }

    private void VisitBlock(BlockNode obj)
    {
        foreach (var node in obj.Children)
        {
            node.Accept(this);
        }
    }
}