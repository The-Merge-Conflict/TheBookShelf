namespace DLMS.Application.DTOs;

public class MediaDto
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string StoragePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string AltText { get; set; } = string.Empty;
    public string? Thumbnail { get; set; }
}