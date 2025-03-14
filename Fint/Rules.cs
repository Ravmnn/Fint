using System.Linq;
using System.Collections.Generic;


namespace Fint;


public class MatchRule(int id, params string[] identifiers) : Rule(id)
{
    public MatchRule(params string[] identifiers)
        : this(0, identifiers) { }


    public override bool Pass(Token token) => identifiers.Contains(token.Text);

    public override IEnumerable<Token> Process(Token[] tokens, ref int index)
        => SetId([tokens[index]]);
}



public class BetweenRule(int id, string left, string right, bool includeDelimiters = false) : Rule(id)
{
    public string Left => left;
    public string Right => right;

    public bool IncludeDelimiters => includeDelimiters;


    public BetweenRule(string left, string right, bool includeDelimiters = false)
        : this(0, left, right, includeDelimiters) { }


    public override bool Pass(Token token) => token.Text == Left;

    public override IEnumerable<Token> Process(Token[] tokens, ref int index)
    {
        var result = new List<Token>();

        if (!IncludeDelimiters)
            index++;

        for (; index < tokens.Length; index++)
        {
            var token = tokens[index];

            result.Add(token);

            if (token.Text != Right)
                continue;

            if (!IncludeDelimiters)
                result.RemoveAt(result.Count - 1);

            break;
        }

        return SetId(result);
    }
}
