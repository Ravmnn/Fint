using System.Collections.Generic;
using System.Linq;


namespace Fint;


/// <summary>
/// Base class for all rules.
/// </summary>
public abstract class Rule(int id)
{
    /// <summary>
    /// A number representing this rule. You can use this to add special meaning to each token
    /// </summary>
    public int Id { get; } = id;


    /// <summary>
    /// Defines the condition that will be used for searching tokens.
    /// </summary>
    /// <param name="token">The token in which the condition will be tested.</param>
    public abstract bool Pass(Token token);

    /// <summary>
    /// The logic that will be used to return matched tokens.
    /// </summary>
    /// <param name="tokens">The tokens source.</param>
    /// <param name="index">The index in which the first token match happened.</param>
    /// <returns>Filtered tokens based on a specific logic.</returns>
    public virtual IEnumerable<Token> Process(Token[] tokens, ref int index)
        => SetId([tokens[index]]);


    protected IEnumerable<Token> SetId(IEnumerable<Token> tokens)
        => from token in tokens select token with { Id = Id };
}
