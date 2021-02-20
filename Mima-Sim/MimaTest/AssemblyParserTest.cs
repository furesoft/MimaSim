﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.Core.AST;
using MimaSim.MIMA;
using MimaSim.MIMA.Parsing;
using MimaSim.MIMA.Parsing.Parsers;

namespace MimaTest
{
    [TestClass]
    public class AssemblyParserTest
    {
        [TestMethod]
        public void NoArgInstruction_Should_Pass()
        {
            var input = "add\nsub";
            var parser = new AssemblyParser();

            var result = parser.Parse(input);

            var toTest = NodeFactory.Call("{}", AstCallNodeType.Group,
                    NodeFactory.Call("noArgInstruction", null, NodeFactory.Literal(OpCodes.ADD)),
                    NodeFactory.Call("noArgInstruction", null, NodeFactory.Literal(OpCodes.SUB))
                );

            Assert.AreEqual(result.ToString(), toTest.ToString());
        }
    }
}