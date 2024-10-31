using Silverfly.Nodes;

namespace MimaSim.MIMA.Parsing.Parsers.High.AST;

public record ReturnStatement(AstNode? Value) : AstNode;