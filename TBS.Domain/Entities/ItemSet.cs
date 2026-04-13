namespace TBS.Domain.Entities;

public class ItemSet : Resource
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsPublic { get; private set; }

    private readonly List<Item> _items = [];
    public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

    private ItemSet() { }

    public static ItemSet Create(string title, string? description, bool isPublic, int? ownerId, string createdBy)
    {
        return new ItemSet
        {
            Title = title,
            Description = description,
            IsPublic = isPublic,
            OwnerId = ownerId,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            Type = "ItemSet"
        };
    }

    public void Update(string title, string? description, bool isPublic, string modifiedBy)
    {
        Title = title;
        Description = description;
        IsPublic = isPublic;
        ModifiedBy = modifiedBy;
        ModifiedAt = DateTime.UtcNow;
    }
}