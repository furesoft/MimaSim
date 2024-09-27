using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.Core.Parsing.AST.Nodes;
using MimaSim.MIMA.Parsing.Parsers;
using MimaSim.MIMA.Visitors;
using System.Linq;

namespace MimaTest;

[TestClass]
public class RawParserTests
{
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

    [TestMethod]
    public void SimpleTest_Should_Pass()
    {
        var input = "2A 90 2A 90 2A 90";
        var parser = new RawParser();
        var ast = (CallNode)parser.Parse(input);

        Assert.AreEqual(ast.Args.Count, 6);
        Assert.AreEqual(input, string.Join(' ', ast.Args.Select(_ =>
        {
            if (_ is LiteralNode ln)
            {
                return ((byte)ln.Value).ToString("x");
            }
            else
            {
                return "0";
            }
        })).ToUpper());
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
}