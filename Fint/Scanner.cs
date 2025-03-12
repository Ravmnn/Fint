using System.Collections.Generic;


namespace Fint;


public class Scanner
{
    private readonly string _source;
    private int _start, _end;

    private List<Token> _tokens = [];


    public Scanner(string source)
    {
        _source = source;
    }


    private void Reset()
    {
        _start = _end = 0;
        _tokens = [];
    }


    public IEnumerable<Token> Scan()
    {
        Reset();

        while (!AtEnd())
        {
            _start = _end;
            ScanToken();
        }

        return _tokens;
    }


    private void ScanToken()
    {
        var ch = Advance();

        if (ch is ' ' or '\n' or '\t' or '\r')
            return;

        if (char.IsAsciiLetter(ch))
            while (!AtEnd() && char.IsAsciiLetterOrDigit(Peek()))
                Advance();

        _tokens.Add(new Token(_start, _end, _source[_start.._end]));
    }


    private char Advance() => _source[_end++];
    private char Peek() => _source[_end];

    private bool AtEnd() => _end >= _source.Length;
}
