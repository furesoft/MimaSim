using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Parsing.SourceTranslators;
using System.Linq;

namespace MimaTest
{
    [TestClass]
    public class ExpressionParserTests
    {
        [TestMethod]
        public void Parse_Literal_Should_Pass()
        {
            var input = "1+2*3";
            var parser = new HighSourceTextTranslator();

            DiagnosticBag diagnostics = new DiagnosticBag();

            var ast = parser.ToRaw(input, ref diagnostics);
        }

        [TestMethod]
        public void RecursiveTraversing()
        {
            var input = "1+2*3";

            var parser = new HighParser();
            var ast = parser.Parse(input);

            int result = TraverseTree(ast);
        }

        private int TraverseTree(IAstNode ast)
        {
            if (ast is LiteralNode lit)
            {
                return (ushort)lit.Value;
            }
            else if (ast is CallNode cn)
            {
                switch (cn.Name)
                {
                    case "+":
                        return TraverseTree(cn.Args.First()) + TraverseTree(cn.Args.Last());

                    case "*":
                        return TraverseTree(cn.Args.First()) * TraverseTree(cn.Args.Last());
                }
            }

            return 0;
        }
    }
}