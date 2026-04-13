using TBS.Domain.Entities;

namespace TBS.Domain.Interfaces.Repositories;

public interface IResourceRepository
{
    Task<Resource?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsAsync(int id, CancellationToken ct = default);
}