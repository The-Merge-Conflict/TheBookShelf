using TBS.Domain.Exceptions;

namespace TBS.Domain.ValueObjects;

public sealed class MimeType
{
    private static readonly HashSet<string> _allowed =
    [
        "image/jpeg", "image/png", "image/gif", "image/webp",
        "application/pdf", "video/mp4", "video/mpeg",
        "audio/mpeg", "audio/wav", "text/plain"
    ];

    public string Value { get; }
    private MimeType(string value) => Value = value;

    public static MimeType Create(string value)
    {
        var normalized = value.ToLowerInvariant().Trim();
        if (!_allowed.Contains(normalized))
            throw new DomainException($"Unsupported MIME type: '{value}'.");
        return new MimeType(normalized);
    }

    public bool IsImage => Value.StartsWith("image/");
    public bool IsVideo => Value.StartsWith("video/");
    public bool IsAudio => Value.StartsWith("audio/");
    public bool IsPdf => Value == "application/pdf";

    public override string ToString() => Value;
    public override bool Equals(object? obj) => obj is MimeType other && Value == other.Value;
    public override int GetHashCode() => Value.GetHashCode();
}