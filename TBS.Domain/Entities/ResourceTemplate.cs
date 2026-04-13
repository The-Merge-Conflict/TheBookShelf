namespace TBS.Domain.Entities;

public class ResourceTemplate
{
    public int Id { get; private set; }
    public string Label { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    private readonly List<TemplateProperty> _templateProperties = [];
    public IReadOnlyCollection<TemplateProperty> TemplateProperties => _templateProperties.AsReadOnly();

    private ResourceTemplate() { }

    public static ResourceTemplate Create(string label, string? description)
    {
        return new ResourceTemplate { Label = label, Description = description };
    }

    public void Update(string label, string? description)
    {
        Label = label;
        Description = description;
    }
}