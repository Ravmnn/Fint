using Fint;


namespace FintTest;


class Program
{
    static void Main()
    {
        var scanner = new Scanner(File.ReadAllText("../../../source.txt"));
        var tokens = scanner.Scan();

        foreach (var token in tokens)
            Console.WriteLine($"\"{token.Text}\"");
    }
}
