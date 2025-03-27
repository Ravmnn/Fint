using System;
using System.Linq;
using System.Collections.Generic;


namespace Fint;


/// <summary>
/// Searches for tokens that matches the strings in "identifiers"
/// </summary>
public class MatchRule(int id, params string[] identifiers) : Rule(id)
{
    public string[] Identifiers => identifiers;


    public MatchRule(params string[] identifiers)
        : this(0, identifiers) { }


    public override bool Pass(Token token) => Identifiers.Contains(token.Text);
}


/// <summary>
/// Searches for tokens inside two string delimiters.
/// </summary>
public class BetweenRule(int id, string left, string right, bool includeDelimiters = false) : Rule(id)
{
    /// <summary>
    /// The left-sided delimiter.
    /// </summary>
    public string Left => left;

    /// <summary>
    /// The right-sided delimiter.
    /// </summary>
    public string Right => right;

    /// <summary>
    /// If true, delimiters will be matched as tokens.
    /// </summary>
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

        return SetId(result, Id);
    }
}


/// <summary>
/// Words (a-z; A-Z) only.
/// </summary>
public class WordRule(int id) : Rule(id)
{
    public override bool Pass(Token token) => token.Text.All(ch => char.IsLetter(ch) || ch == '_');
}

/// <summary>
/// Words (a-z; A-Z) and digits (0-9) only.
/// </summary>
public class WordOrDigitRule(int id) : Rule(id)
{
    public override bool Pass(Token token) => token.Text.All(ch => char.IsLetterOrDigit(ch) || ch == '_');
}

/// <summary>
/// Digits (0-9) only.
/// </summary>
public class DigitRule(int id) : Rule(id)
{
    public override bool Pass(Token token) => token.Text.All(char.IsDigit);
}

/// <summary>
/// Anything different from a word (a-z; A-Z).
/// </summary>
public class NonWordRule(int id) : Rule(id)
{
    public override bool Pass(Token token) => token.Text.All(ch => !char.IsLetter(ch) && ch != '_');
}

/// <summary>
/// Anything different from a word (a-z; A-Z) and digit (0-9).
/// </summary>
public class NonWordAndDigitRule(int id) : Rule(id)
{
    public override bool Pass(Token token) => token.Text.All(ch => !char.IsLetterOrDigit(ch) && ch != '_');
}


/// <summary>
/// Custom condition based rule.
/// </summary>
public class ConditionalRule(int id, Func<Token, bool> condition) : Rule(id)
{
    /// <summary>
    /// The condition as a lambda.
    /// </summary>
    public Func<Token, bool> Condition => condition;


    public override bool Pass(Token token) => Condition(token);
}
