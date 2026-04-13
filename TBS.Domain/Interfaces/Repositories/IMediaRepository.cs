using TBS.Domain.Entities;

namespace TBS.Domain.Interfaces.Repositories;

public interface IMediaRepository : IRepository<Media>
{
    Task<IEnumerable<Media>> GetByItemIdAsync(int itemId, CancellationToken ct = default);
    Task<Media?> GetByStoragePathAsync(string storagePath, CancellationToken ct = default);
}