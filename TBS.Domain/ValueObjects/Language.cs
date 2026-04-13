using TBS.Domain.Exceptions;

namespace TBS.Domain.ValueObjects;

// BCP 47 language codes — e.g. "en", "ar", "fr"
public sealed class Language
{
    public string Code { get; }
    private Language(string code) => Code = code;

    public static Language Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length < 2 || code.Length > 10)
            throw new DomainException($"'{code}' is not a valid BCP 47 language code.");
        return new Language(code.ToLowerInvariant().Trim());
    }

    public override string ToString() => Code;
    public override bool Equals(object? obj) => obj is Language other && Code == other.Code;
    public override int GetHashCode() => Code.GetHashCode();
}