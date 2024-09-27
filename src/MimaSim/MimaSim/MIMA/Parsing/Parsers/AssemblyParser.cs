using MimaSim.Core;
using MimaSim.Core.Parsing;
using MimaSim.Core.Parsing.AST;
using MimaSim.Core.Parsing.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.MIMA.Parsing.Parsers;

public class AssemblyParser : IParser
{
    public DiagnosticBag Diagnostics = new();
    private TokenEnumerator _enumerator;

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
        tokenizer.AddDefinition(TokenKind.LabelReference, @"\$[a-zA-Z_][0-9a-zA-F_]*", 4);

        tokenizer.AddDefinition(TokenKind.Comment, @"/\\*.*?\\*/", 1);

        var tokens = tokenizer.Tokenize(input);
        _enumerator = new TokenEnumerator(tokens);

        return ParseInstructionBlock();
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

    private IAstNode ParseInstruction()
    {
        var mnemnonic = _enumerator.Read(TokenKind.Mnemnonic);
        var opcode = Enum.Parse<OpCodes>(mnemnonic.Contents, true);

        switch (opcode)
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

            case OpCodes.NOT:
            case OpCodes.AND:
            case OpCodes.OR:
            case OpCodes.XOR:

            case OpCodes.CMPEQ:
            case OpCodes.CMPGE:
            case OpCodes.CMPGT:
            case OpCodes.CMPLE:
            case OpCodes.CMPLT:
                return NodeFactory.Call(AstCallNodeType.NoArgInstruction, NodeFactory.Literal(opcode));

            case OpCodes.LOAD:
                return NodeFactory.Call(AstCallNodeType.Load, ParseLiteral());

            case OpCodes.MOV:
                return ParseMoveInstruction();

            case OpCodes.JMP:
                var label = _enumerator.Read(TokenKind.LabelReference);

                return NodeFactory.Call(AstCallNodeType.Jmp, NodeFactory.Literal(label.Contents));

            case OpCodes.JMPC:
                var labelc = _enumerator.Read(TokenKind.LabelReference);

                return NodeFactory.Call(AstCallNodeType.JmpConditional, NodeFactory.Literal(labelc.Contents));
        }

        return null;
    }

    private IAstNode ParseInstructionBlock()
    {
        var _nodes = new List<IAstNode>();
        Token token;
        do
        {
            token = _enumerator.Peek();

            if (token.Kind == TokenKind.Mnemnonic)
            {
                _nodes.Add(ParseInstruction());
            }
            else if (token.Kind == TokenKind.Identifier)
            {
                _nodes.Add(ParseLabel());
            }
            else if (token.Kind == TokenKind.EndOfFile)
            {
                break;
            }
        } while (token.Kind != TokenKind.EndOfFile);

        return NodeFactory.Call(AstCallNodeType.Group, _nodes.ToArray());
    }

    private IAstNode ParseLabel()
    {
        var nameToken = _enumerator.Read(TokenKind.Identifier);
        _enumerator.Read(TokenKind.Colon);

        return NodeFactory.Call(AstCallNodeType.Label, NodeFactory.Id(nameToken.Contents));
    }

    private IAstNode ParseLiteral()
    {
        var token = _enumerator.Read();

        if (token.Kind == TokenKind.HexLiteral)
        {
            var value = Convert.ToInt16(token.Contents, 16);

            return NodeFactory.Literal(value);
        }
        else
        {
            Diagnostics.ReportHexLiteralExpected(token);

            return NodeFactory.Literal(null);
        }
    }

    private IAstNode ParseMoveInstruction()
    {
        var firstArg = _enumerator.Read();

        _enumerator.Read(TokenKind.Comma);

        var secondArg = _enumerator.Read();

        if (firstArg.Kind == TokenKind.Register && secondArg.Kind == TokenKind.Register)
        {
            var firstArgRegister = Enum.Parse<Registers>(firstArg.Contents, true);
            var secondArgRegister = Enum.Parse<Registers>(secondArg.Contents, true);

            return NodeFactory.Call(AstCallNodeType.MovRegReg,
                NodeFactory.Literal(firstArgRegister),
                NodeFactory.Literal(secondArgRegister)
            );
        }
        else if (firstArg.Kind == TokenKind.Register && secondArg.Kind == TokenKind.AddressLiteral)
        {
            var register = Enum.Parse<Registers>(firstArg.Contents, true);

            return NodeFactory.Call(AstCallNodeType.MovRegMem,
                NodeFactory.Literal(register),
                NodeFactory.Literal(Convert.ToInt16(secondArg.Contents.Remove(0, 1), 16)));
        }
        else if (firstArg.Kind == TokenKind.AddressLiteral && secondArg.Kind == TokenKind.Register)
        {
            var register = Enum.Parse<Registers>(secondArg.Contents, true);

            return NodeFactory.Call(AstCallNodeType.MovMemReg,
                NodeFactory.Literal(Convert.ToUInt16(firstArg.Contents.Remove(0, 1), 16)),
                NodeFactory.Literal(register));
        }
        else if (firstArg.Kind == TokenKind.AddressLiteral && secondArg.Kind == TokenKind.AddressLiteral)
        {
            return NodeFactory.Call(AstCallNodeType.MovMemMem,
                NodeFactory.Literal(Convert.ToUInt16(firstArg.Contents.Remove(0, 1), 16)),
                NodeFactory.Literal(Convert.ToUInt16(secondArg.Contents.Remove(0, 1), 16)));
        }
        else
        {
            Diagnostics.ReportInvalidMovInstruction(firstArg.Start, firstArg.End);
        }

        return NodeFactory.Call(AstCallNodeType.Group);
    }
}