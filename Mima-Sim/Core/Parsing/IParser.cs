using MimaSim.Core.Parsing.AST;

namespace MimaSim.Core.Parsing
{
    public interface IParser
    {
        IAstNode Parse(string input);
    }
}