using System.Collections.Immutable;
using Silverfly;
using Silverfly.Helpers;
using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.High.AST;

public record FuncDefNode(Token Name, TypeName Type, ImmutableList<AstNode> Parameters, ImmutableList<AstNode> Body) : AstNode;