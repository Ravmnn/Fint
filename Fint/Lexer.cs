using System.Collections.Generic;


namespace Fint;


/// <summary>
/// Converts a full string into tokens.
/// </summary>
public class Lexer(string source)
{
    private int _start, _end;

    private List<Token> _tokens = [];


    private void Reset()
    {
        _start = _end = 0;
        _tokens = [];
    }


    public IEnumerable<Token> Tokenize()
    {
        Reset();

        while (!AtEnd())
        {
            _start = _end;
            TokenizeNext();
        }

        return _tokens;
    }


    private void TokenizeNext()
    {
        var ch = Advance();

        if (ch is ' ' or '\n' or '\t' or '\r')
            return;

        if (char.IsAsciiLetter(ch))
            while (!AtEnd() && char.IsAsciiLetterOrDigit(Peek()))
                Advance();

        _tokens.Add(new Token(_start, _end, source[_start.._end]));
    }


    private char Advance() => source[_end++];
    private char Peek() => source[_end];

    private bool AtEnd() => _end >= source.Length;
}
