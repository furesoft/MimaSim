using System.Collections.Immutable;
using Silverfly;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.High.AST;

public record FuncDefNode(Token Name, ImmutableList<AstNode> Parameters, ImmutableList<AstNode> Body) : AstNode;