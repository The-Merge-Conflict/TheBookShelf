namespace TBS.Application.Features.Media.Queries.GetMediaByItem;

public record MediaDto(
    int Id,
    string FileName,
    string MimeType,
    string StoragePath,
    string FileSize,      
    string? AltText,
    bool IsImage,
    bool IsPdf,
    DateTime CreatedAt
);