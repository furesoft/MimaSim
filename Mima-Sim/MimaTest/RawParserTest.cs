using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}