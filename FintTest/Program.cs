using Fint;


namespace FintTest;


class Program
{
    public static void Main()
    {
        var lexer = new Lexer(File.ReadAllText("../../../source.txt"));
        var scanner = new Scanner(new MatchRule(1, "target"), new BetweenRule(2, "\"", "\""));
        var tokens = lexer.Tokenize();

        tokens = scanner.Scan(tokens);

        foreach (var token in tokens)
            Console.WriteLine($"\"{token.Text}\", - location: {token.Start};{token.End}, id: {token.Id}");
    }
}
