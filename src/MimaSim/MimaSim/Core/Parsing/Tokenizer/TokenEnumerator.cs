using System;
using System.Collections.Generic;
using System.Linq;

namespace MimaSim.Core.Parsing.Tokenizer;

public sealed class TokenEnumerator(IEnumerable<Token> tokenStream)
{
    private readonly Token[] _tokens = tokenStream.ToArray();
    private int _position;

    public Token Current
    {
        get
        {
            if (_position < _tokens.Length) return _tokens[_position];

            return Token.EndOfFile;
        }
    }

    public void BackTrack(int pos)
    {
        _position = pos;
    }

    public int BackTrackPos()
    {
        return _position;
    }

    public Token Peek(int offset = 0)
    {
        if (_position + offset <= _tokens.Length - 1) return _tokens[_position + offset];

        return Token.EndOfFile;
    }

    public Token Read()
    {
        var result = Peek();

        if (result.Kind == TokenKind.Comment)
        {
            Advance(1);
            result = Peek();
        }

        Advance(1);
        return result;
    }

    public Token Read(TokenKind Kind)
    {
        var result = Read();
        if (result.Kind != Kind)
            throw new Exception(
                "Expected a token of type '" + Kind +
                "', got '" + result.Contents + "'.");

        return result;
    }

    private void Advance(int offset)
    {
        _position += offset;
    }
}