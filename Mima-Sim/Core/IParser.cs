using MimaSim.Core.AST;

namespace MimaSim.Core
{
    public interface IParser
    {
        IAstNode Parse(string input);
    }
}