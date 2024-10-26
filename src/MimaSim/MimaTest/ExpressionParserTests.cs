using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.Core;
using MimaSim.MIMA.Parsing.Parsers.High;

namespace MimaTest;

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
}