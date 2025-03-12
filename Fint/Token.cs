namespace Fint;


public readonly record struct Token(int Start, int End, string Text, int? Id = null);
