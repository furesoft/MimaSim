using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.Tokenizer;
using System;

namespace MimaSim.MIMA.Parsing.Parsers
{
    public class HighParser : IParser
    {
        public DiagnosticBag Diagnostics = new DiagnosticBag();
        private TokenEnumerator _enumerator;

        public IAstNode Parse(string input)
        {
            var tokenizer = new PrecedenceBasedRegexTokenizer();

            tokenizer.AddDefinition(TokenKind.HexLiteral, "0x[0-9a-fA-F]{1,6}", 3);
            tokenizer.AddDefinition(TokenKind.IntLiteral, "[0-9]+", 3);
            tokenizer.AddDefinition(TokenKind.Identifier, "[a-zA-Z_][0-9a-zA-F_]*", 4);
            tokenizer.AddDefinition(TokenKind.Operator, @"[+\-*/]", 4);

            tokenizer.AddDefinition(TokenKind.OpenParen, @"\(", 4);
            tokenizer.AddDefinition(TokenKind.CloseParen, @"\)", 4);

            tokenizer.AddDefinition(TokenKind.Comment, @"/\\*.*?\\*/", 1);

            var tokens = tokenizer.Tokenize(input);
            var enumerator = new TokenEnumerator(tokens);

            _enumerator = enumerator;

            return ParseExpression();
        }

        //Expr -> Term + Expr | Term - Expr | Term
        private IAstNode ParseExpression()
        {
            IAstNode left, right = null;

            left = ParseTerm();

            var lookahead = _enumerator.Peek(0);

            if (lookahead.Kind == TokenKind.Operator)
            {
                if (lookahead.Contents == "+" || lookahead.Contents == "-")
                {
                    _enumerator.Read();

                    right = ParseExpression();

                    return NodeFactory.Call(lookahead.Contents, null, left, right);
                }
            }

            return left;
        }

        //Fact -> ( Expr ) | Literal
        private IAstNode ParseFact()
        {
            var lookahead = _enumerator.Peek(0);

            if (lookahead.Kind == TokenKind.OpenParen)
            {
                _enumerator.Read();
                var expr = ParseExpression();
                _enumerator.Read(TokenKind.CloseParen);

                return expr;
            }
            else if (lookahead.Kind == TokenKind.IntLiteral || lookahead.Kind == TokenKind.HexLiteral)
            {
                return ParseLiteral();
            }
            else
            {
                Diagnostics.ReportInvalidSymbol(lookahead);
            }

            return NodeFactory.Literal(null);
        }

        // Literal -> HexLiteral | IntLiteral
        private IAstNode ParseLiteral()
        {
            var token = _enumerator.Read();

            if (token.Kind == TokenKind.HexLiteral)
            {
                return NodeFactory.Literal(Convert.ToUInt16(token.Contents, 16));
            }
            else if (token.Kind == TokenKind.IntLiteral)
            {
                return NodeFactory.Literal(uint.Parse(token.Contents));
            }
            else
            {
                Diagnostics.ReportInvalidLiteral(token);

                return NodeFactory.Literal(null);
            }
        }

        // Term -> Fact * Term | Fact / Term | Fact
        private IAstNode ParseTerm()
        {
            IAstNode left, right = null;
            left = ParseFact();

            var lookahead = _enumerator.Peek(0);

            if (lookahead.Kind == TokenKind.Operator)
            {
                if (lookahead.Contents == "*" || lookahead.Contents == "/")
                {
                    _enumerator.Read();

                    right = ParseTerm();

                    return NodeFactory.Call(lookahead.Contents, null, left, right);
                }
            }

            return left;
        }
    }
}