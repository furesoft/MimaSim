using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.Core.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;

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
        public void WrongInput_Should_Pass()
        {
            var input = "2 A 9 0 2A 90 2A 90";
            var parser = new RawParser();
            var ast = (CallNode)parser.Parse(input);

            if (ast.IsEmpty)
            {
                throw new System.Exception("failed");
            }
        }
    }
}