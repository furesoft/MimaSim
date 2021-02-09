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

            tokenizer.AddDefinition(TokenKind.Comma, ",", 1);
            tokenizer.AddDefinition(TokenKind.AddressLiteral, "&[0-9a-fA-F]{1,6}", 3);
            tokenizer.AddDefinition(TokenKind.HexLiteral, "0x[0-9a-fA-F]{1,6}", 3);
            tokenizer.AddDefinition(TokenKind.Register, GetRegisterPattern(), 2);
            tokenizer.AddDefinition(TokenKind.Mnemnonic, GetMnemnonicPattern(), 2);

            tokenizer.AddDefinition(TokenKind.Comment, @"/\\*.*?\\*/", 1);

            var tokens = tokenizer.Tokenize(input);
            var enumerator = new TokenEnumerator(tokens);

            return ParseInstructionBlock(enumerator);
        }

        private string GetMnemnonicPattern()
        {
            var names = Enum.GetNames(typeof(Mnemnonics));
            var namesLowered = names.Select(_ => _.ToLower());

            var allNames = names.Concat(namesLowered);

            return string.Join("|", allNames);
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
            var token = enumerator.Read();

            return NodeFactory.Call("{}", null);
        }
    }
}