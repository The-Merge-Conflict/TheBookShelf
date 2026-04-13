using TBS.Domain.Entities;

namespace TBS.Domain.Interfaces.Repositories;

public interface IPropertyRepository : IRepository<Property>
{
    Task<IEnumerable<Property>> GetByVocabularyIdAsync(int vocabularyId, CancellationToken ct = default);
    Task<Property?> GetByTermUriAsync(string termUri, CancellationToken ct = default);
}