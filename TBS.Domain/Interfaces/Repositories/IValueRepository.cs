using TBS.Domain.Entities;

namespace TBS.Domain.Interfaces.Repositories;

public interface IValueRepository : IRepository<Value>
{
    Task<IEnumerable<Value>> GetByResourceIdAsync(int resourceId, CancellationToken ct = default);
    Task<IEnumerable<Value>> GetByResourceAndPropertyAsync(int resourceId, int propertyId, CancellationToken ct = default);
    Task DeleteByResourceIdAsync(int resourceId, CancellationToken ct = default);
}