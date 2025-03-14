using System.Collections.Generic;


namespace Fint;


public abstract class Rule(int id)
{
    public int Id { get; } = id;


    public abstract bool Pass(Token token);
    public abstract IEnumerable<Token> Process(Token[] token, ref int index);


    protected IEnumerable<Token> SetId(IEnumerable<Token> tokens)
    {
        var result = new List<Token>();

        foreach (var token in tokens)
            result.Add(token with { Id = Id });

        return result;
    }
}
