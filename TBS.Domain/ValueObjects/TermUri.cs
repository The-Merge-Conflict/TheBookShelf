using TBS.Domain.Exceptions;

namespace TBS.Domain.ValueObjects;

public sealed class TermUri
{
    public string Value { get; }
    private TermUri(string value) => Value = value;

    public static TermUri Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Term URI cannot be empty.");
        if (!Uri.TryCreate(value, UriKind.Absolute, out _))
            throw new DomainException($"'{value}' is not a valid absolute URI.");
        return new TermUri(value);
    }

    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is TermUri other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}