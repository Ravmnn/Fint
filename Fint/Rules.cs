using System.Linq;


namespace Fint;


public class MatchRule(params string[] identifiers) : Rule
{
    public override bool Pass(Token token) => identifiers.Contains(token.Text);
}
