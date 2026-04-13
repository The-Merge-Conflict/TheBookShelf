using TBS.Domain.Entities;

namespace TBS.Domain.Interfaces.Repositories;

public interface IVocabularyRepository : IRepository<Vocabulary>
{
    Task<Vocabulary?> GetByPrefixAsync(string prefix, CancellationToken ct = default);
    Task<Vocabulary?> GetWithPropertiesAsync(int id, CancellationToken ct = default);
    Task<bool> PrefixExistsAsync(string prefix, CancellationToken ct = default);
}