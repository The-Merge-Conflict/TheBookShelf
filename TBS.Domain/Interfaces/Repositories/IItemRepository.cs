using TBS.Domain.Entities;

namespace TBS.Domain.Interfaces.Repositories;

public interface IItemRepository : IRepository<Item>
{
    Task<Item?> GetWithValuesAndMediaAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Item>> GetByItemSetIdAsync(int itemSetId, CancellationToken ct = default);
    Task<IEnumerable<Item>> GetByTemplateIdAsync(int templateId, CancellationToken ct = default);
    Task<IEnumerable<Item>> GetByOwnerIdAsync(int ownerId, CancellationToken ct = default);
}