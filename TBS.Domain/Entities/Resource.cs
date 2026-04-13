using TBS.Domain.Events;

namespace TBS.Domain.Entities;

public abstract class Resource
{
    public int Id { get; protected set; }
    public string Type { get; protected set; } = string.Empty; // TPH discriminator
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public int? OwnerId { get; set; } // FK → AspNetUsers (no nav property, cross-layer)

    private readonly List<Value> _values = [];
    public IReadOnlyCollection<Value> Values => _values.AsReadOnly();

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}