using MimaSim.Core;
using MimaSim.Core.AST;
using MimaSim.Core.Tokenizer;
using MimaSim.MIMA.VM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Parsing.Parsers
{
    public class AssemblyParser : IParser
    {
        public DiagnosticBag Diagnostics = new DiagnosticBag();

        public IAstNode Parse(string input)
        {
            var tokenizer = new PrecedenceBasedRegexTokenizer();

            tokenizer.AddDefinition(TokenKind.Comma, ",", 1);
            tokenizer.AddDefinition(TokenKind.Colon, ":", 1);
            tokenizer.AddDefinition(TokenKind.AddressLiteral, "&[0-9a-fA-F]{1,6}", 3);
            tokenizer.AddDefinition(TokenKind.HexLiteral, "0x[0-9a-fA-F]{1,6}", 3);
            tokenizer.AddDefinition(TokenKind.Register, GetRegisterPattern(), 2);
            tokenizer.AddDefinition(TokenKind.Mnemnonic, GetMnemnonicPattern(), 2);
            tokenizer.AddDefinition(TokenKind.Identifier, "[a-zA-Z_][0-9a-zA-F_]*", 4);

            tokenizer.AddDefinition(TokenKind.Comment, @"/\\*.*?\\*/", 1);

            var tokens = tokenizer.Tokenize(input);
            var enumerator = new TokenEnumerator(tokens);

            return ParseInstructionBlock(enumerator);
        }

        private string GetMnemnonicPattern()
        {
            var names = Enum.GetNames(typeof(OpCodes));
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

        private IAstNode ParseInstruction(TokenEnumerator enumerator)
        {
            var mnemnonic = enumerator.Read(TokenKind.Mnemnonic);
            var value = Enum.Parse<OpCodes>(mnemnonic.Contents, true);

            switch (value)
            {
                case OpCodes.INC:
                case OpCodes.DEC:
                case OpCodes.EXIT:
                case OpCodes.ADD:
                case OpCodes.MUL:
                case OpCodes.SUB:
                case OpCodes.DIV:

                case OpCodes.LSHIFT:
                case OpCodes.RSHIFT:
                    return NodeFactory.Call("noArgInstruction", null, NodeFactory.Literal(value));

                case OpCodes.LOAD:
                    return NodeFactory.Call("load", null, ParseLiteral(enumerator));

                case OpCodes.MOV:
                    return ParseMoveInstruction(enumerator);

                case OpCodes.NOT:
                    break;

                case OpCodes.AND:
                    break;

                case OpCodes.OR:
                    break;

                case OpCodes.XOR:
                    break;

                case OpCodes.JUMP:
                    break;

                case OpCodes.JNEQ:
                    break;

                case OpCodes.JEQ:
                    break;

                case OpCodes.JLT:
                    break;

                case OpCodes.JLE:
                    break;

                case OpCodes.JGT:
                    break;

                case OpCodes.JGE:
                    break;
            }

            return null;
        }

        private IAstNode ParseInstructionBlock(TokenEnumerator enumerator)
        {
            //ToDo: implement assembly parser
            var _nodes = new List<IAstNode>();
            Token token;
            do
            {
                token = enumerator.Peek();

                if (token.Kind == TokenKind.Mnemnonic)
                {
                    _nodes.Add(ParseInstruction(enumerator));
                }
                else if (token.Kind == TokenKind.Identifier)
                {
                    _nodes.Add(ParseLabel(enumerator));
                }
                else if (token.Kind == TokenKind.EndOfFile)
                {
                    break;
                }
            } while (token.Kind != TokenKind.EndOfFile);

            return NodeFactory.Call("{}", AstCallNodeType.Group, _nodes.ToArray());
        }

        private IAstNode ParseLabel(TokenEnumerator enumerator)
        {
            var nameToken = enumerator.Read(TokenKind.Identifier);
            enumerator.Read(TokenKind.Colon);

            return NodeFactory.Call("label", null, NodeFactory.Id(nameToken.Contents));
        }

        private IAstNode ParseLiteral(TokenEnumerator enumerator)
        {
            var token = enumerator.Read(TokenKind.HexLiteral);

            var value = Convert.ToUInt16(token.Contents, 16);

            return NodeFactory.Literal(value);
        }

        private IAstNode ParseMoveInstruction(TokenEnumerator enumerator)
        {
            var firstArg = enumerator.Read();

            enumerator.Read(TokenKind.Comma);

            var secondArg = enumerator.Read();

            if (firstArg.Kind == TokenKind.Register && secondArg.Kind == TokenKind.Register)
            {
                var firstArgRegister = Enum.Parse<Registers>(firstArg.Contents, true);
                var secondArgRegister = Enum.Parse<Registers>(secondArg.Contents, true);

                return NodeFactory.Call("mov", null,
                    NodeFactory.Literal(firstArgRegister),
                    NodeFactory.Literal(secondArgRegister)
                );
            }
            else
            {
                Diagnostics.ReportInvalidMovInstruction(firstArg.Start, firstArg.End);
            }

            return NodeFactory.Call("{}", null);
        }
    }
}