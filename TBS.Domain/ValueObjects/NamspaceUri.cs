using TBS.Domain.Exceptions;

namespace TBS.Domain.ValueObjects;

public sealed class NamespaceUri
{
    public string Value { get; }
    private NamespaceUri(string value) => Value = value;

    public static NamespaceUri Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Namespace URI cannot be empty.");
        if (!Uri.TryCreate(value, UriKind.Absolute, out _))
            throw new DomainException($"'{value}' is not a valid namespace URI.");
        return new NamespaceUri(value);
    }

    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is NamespaceUri other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}