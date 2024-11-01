using Silverfly.Nodes;
using Silverfly.Text;

namespace MimaSim.MIMA.Parsing.Parsers.High;

public class PassContext
{
    public AstNode Tree;
    public SourceDocument Document;
    public string Input;
    public Scope Scope;
    public byte[] Program;
}