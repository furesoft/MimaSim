using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.MIMA.Parsing.Parsers;

namespace MimaTest
{
    [TestClass]
    public class AssemblyParserTest
    {
        [TestMethod]
        public void Tokenize_Should_Pass()
        {
            var input = "/* hello world. My name is mov */mov 0x42, X";
            var parser = new AssemblyParser();

            var result = parser.Parse(input);
        }
    }
}