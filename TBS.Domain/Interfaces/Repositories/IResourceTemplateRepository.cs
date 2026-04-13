using TBS.Domain.Entities;

namespace TBS.Domain.Interfaces.Repositories;

public interface IResourceTemplateRepository : IRepository<ResourceTemplate>
{
    Task<ResourceTemplate?> GetWithPropertiesAsync(int id, CancellationToken ct = default);
    Task<bool> LabelExistsAsync(string label, CancellationToken ct = default);
}