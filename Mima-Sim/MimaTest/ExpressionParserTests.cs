using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Parsing.SourceTranslators;

namespace MimaTest
{
    [TestClass]
    public class ExpressionParserTests
    {
        [TestMethod]
        public void Parse_Literal_Should_Pass()
        {
            var input = "1 + 2 * 2 + 3";
            var parser = new HighSourceTextTranslator();

            DiagnosticBag diagnostics = new DiagnosticBag();

            var ast = parser.ToRaw(input, ref diagnostics);
        }
    }
}