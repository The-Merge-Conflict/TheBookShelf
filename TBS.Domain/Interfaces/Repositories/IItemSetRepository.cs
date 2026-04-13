using TBS.Domain.Entities;

namespace TBS.Domain.Interfaces.Repositories;

public interface IItemSetRepository : IRepository<ItemSet>
{
    Task<ItemSet?> GetWithItemsAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<ItemSet>> GetPublicAsync(CancellationToken ct = default);
}