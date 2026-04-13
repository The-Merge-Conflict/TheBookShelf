using TBS.Domain.Exceptions;

namespace TBS.Domain.ValueObjects;

public sealed class FileSize
{
    public long Bytes { get; }
    private FileSize(long bytes) => Bytes = bytes;

    public static FileSize Create(long bytes)
    {
        if (bytes < 0)
            throw new DomainException("File size cannot be negative.");
        return new FileSize(bytes);
    }

    public double ToKilobytes() => Math.Round(Bytes / 1024.0, 2);
    public double ToMegabytes() => Math.Round(Bytes / (1024.0 * 1024), 2);

    public string ToHumanReadable() => Bytes switch
    {
        < 1024 => $"{Bytes} B",
        < 1024 * 1024 => $"{ToKilobytes()} KB",
        _ => $"{ToMegabytes()} MB"
    };

    public override string ToString() => ToHumanReadable();
    public override bool Equals(object? obj) => obj is FileSize other && Bytes == other.Bytes;
    public override int GetHashCode() => Bytes.GetHashCode();
}