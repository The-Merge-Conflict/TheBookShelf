using TBS.Domain.Events;
using TBS.Domain.ValueObjects;

namespace TBS.Domain.Entities;

public class Media : Resource
{
    public int ItemId { get; private set; }
    public Item Item { get; private set; } = null!;
    public string StoragePath { get; private set; } = string.Empty;
    public string FileName { get; private set; } = string.Empty;
    public MimeType MimeType { get; private set; } = null!;
    public FileSize FileSize { get; private set; } = null!;
    public string? AltText { get; private set; }

    private Media() { }

    public static Media Create(
        int itemId, string storagePath, string fileName,
        string mimeType, long fileSize, string? altText,
        int? ownerId, string createdBy)
    {
        var media = new Media
        {
            ItemId = itemId,
            StoragePath = storagePath,
            FileName = fileName,
            MimeType = MimeType.Create(mimeType),
            FileSize = FileSize.Create(fileSize),
            AltText = altText,
            OwnerId = ownerId,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            Type = "Media"
        };
        media.AddDomainEvent(new MediaUploadedEvent(media));
        return media;
    }
}