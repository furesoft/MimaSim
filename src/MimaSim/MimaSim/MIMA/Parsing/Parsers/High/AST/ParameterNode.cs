using Silverfly;
using Silverfly.Helpers;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.High.AST;

public record ParameterNode(Token Name, TypeName Type) : AstNode;