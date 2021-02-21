using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.MIMA.Parsing.Parsers;

namespace MimaTest
{
    [TestClass]
    public class ExpressionParserTests
    {
        [TestMethod]
        public void Parse_Literal_Should_Pass()
        {
            var input = "1 + 2 * 2 + 3";
            var parser = new HighParser();

            var ast = parser.Parse(input);
        }
    }
}