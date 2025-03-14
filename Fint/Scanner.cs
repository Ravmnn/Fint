using System.Linq;
using System.Collections.Generic;


namespace Fint;


public class Scanner(params IEnumerable<Rule> rules)
{
    public IEnumerable<Rule> Rules { get; } = rules;


    public IEnumerable<Token> Scan(IEnumerable<Token> tokens)
    {
        var result = new List<Token>();
        var enumerableTokens = tokens as Token[] ?? tokens.ToArray();

        for (var i = 0; i < enumerableTokens.Length; i++)
        {
            var token = enumerableTokens[i];

            foreach (var rule in Rules)
            {
                if (!rule.Pass(token))
                    continue;

                result.AddRange(rule.Process(enumerableTokens, ref i));
                break;
            }
        }

        return result;
    }
}
