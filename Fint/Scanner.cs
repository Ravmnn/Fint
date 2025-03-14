using System.Linq;
using System.Collections.Generic;


namespace Fint;


public class Scanner(int id, params IEnumerable<Rule> rules)
{
    public IEnumerable<Rule> Rules { get; } = rules;
    public int Id { get; } = id;


    public Scanner(params IEnumerable<Rule> rules) : this(0, rules.ToList())
    {}


    public IEnumerable<Token> Scan(IEnumerable<Token> tokens)
    {
        var result = new List<Token>();
        var enumerable = tokens as Token[] ?? tokens.ToArray();

        foreach (var token in enumerable)
            if (Rules.Any(rule => rule.Pass(token)))
                result.Add(token with { Id = Id });

        return result;
    }
}
