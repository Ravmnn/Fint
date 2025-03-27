using System.Globalization;


namespace Fint;


/// <summary>
/// Represents a substring with extra data.
/// </summary>
/// <param name="Start">The start index of the token.</param>
/// <param name="End">The end index of the token.</param>
/// <param name="Text">The literal substring.</param>
/// <param name="Id">Rules sets this to its own id.</param>
public readonly record struct Token(int Start, int End, string Text, int? Id = null);
