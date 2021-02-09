using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.Tokenizer;
using MimaSim.MIMA.VM;
using System;
using System.Linq;

namespace MimaSim.MIMA.Parsing.Parsers
{
    public class AssemblyParser : IParser
    {
        public IAstNode Parse(string input)
        {
            var tokenizer = new PrecedenceBasedRegexTokenizer();
            tokenizer.AddDefinition(TokenKind.AddressLiteral, "&[0-9a-fA-F]{1,6}", 2);
            tokenizer.AddDefinition(TokenKind.HexLiteral, "0x[0-9a-fA-F]{1,6}", 2);
            tokenizer.AddDefinition(TokenKind.Register, GetRegisterPattern(), 1);

            var tokens = tokenizer.Tokenize(input);
            var enumerator = new TokenEnumerator(tokens);

            return ParseInstructionBlock(enumerator);
        }

        private string GetRegisterPattern()
        {
            var names = Enum.GetNames(typeof(Registers));
            var namesLowered = names.Select(_ => _.ToLower());

            var allNames = names.Concat(namesLowered);

            return string.Join("|", allNames);
        }

        private IAstNode ParseInstructionBlock(TokenEnumerator enumerator)
        {
            //ToDo: implement assembly parser
            return NodeFactory.Call("{}", null);
        }
    }
}