using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.Core.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Visitors;

namespace MimaTest
{
    [TestClass]
    public class RawParserTests
    {
        [TestMethod]
        public void SimpleTest_Should_Pass()
        {
            var input = "2A 90 2A 90 2A 90";
            var parser = new RawParser();
            var ast = parser.Parse(input);
        }

        [TestMethod]
        public void WrongInput_Should_Fail()
        {
            var input = "2 A 9 0 2A 90 2A 90";
            var parser = new RawParser();
            var ast = (CallNode)parser.Parse(input);

            if (ast.IsEmpty)
            {
                throw new System.Exception("failed");
            }
        }

        [TestMethod]
        public void Input_Produce_Raw_Should_Pass()
        {
            var input = "2A 90 2A 90 2A 90";
            var parser = new RawParser();
            var ast = (CallNode)parser.Parse(input);
            var visitor = new RawParserVisitor();

            ast.Visit(visitor);

            Assert.AreEqual(visitor.GetRaw().Length, 6);

            if (ast.IsEmpty)
            {
                throw new System.Exception("failed");
            }
        }
    }
}