using TBS.Domain.Events;

namespace TBS.Domain.Entities;

public class Item : Resource
{
    public int? TemplateId { get; private set; }
    public ResourceTemplate? Template { get; private set; }

    private readonly List<Media> _media = [];
    public IReadOnlyCollection<Media> Media => _media.AsReadOnly();

    private readonly List<ItemSet> _itemSets = [];
    public IReadOnlyCollection<ItemSet> ItemSets => _itemSets.AsReadOnly(); // many-to-many

    private Item() { }

    public static Item Create(int? templateId, int? ownerId, string createdBy)
    {
        var item = new Item
        {
            TemplateId = templateId,
            OwnerId = ownerId,
            CreatedBy = createdBy,
            CreatedAt = DateTime.UtcNow,
            Type = "Item"
        };
        item.AddDomainEvent(new ResourceCreatedEvent(item));
        return item;
    }

    public void Update(int? templateId, string modifiedBy)
    {
        TemplateId = templateId;
        ModifiedBy = modifiedBy;
        ModifiedAt = DateTime.UtcNow;
    }
}