using TBS.Domain.ValueObjects;

namespace TBS.Domain.Entities;

public class Vocabulary
{
    public int Id { get; private set; }
    public string Prefix { get; private set; } = string.Empty; // e.g. "dc", "foaf"
    public NamespaceUri NamespaceUri { get; private set; } = null!;
    public string Label { get; private set; } = string.Empty;

    private readonly List<Property> _properties = [];
    public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();

    private Vocabulary() { }

    public static Vocabulary Create(string prefix, string namespaceUri, string label)
    {
        return new Vocabulary
        {
            Prefix = prefix,
            NamespaceUri = NamespaceUri.Create(namespaceUri),
            Label = label
        };
    }

    public void Update(string prefix, string namespaceUri, string label)
    {
        Prefix = prefix;
        NamespaceUri = NamespaceUri.Create(namespaceUri);
        Label = label;
    }
}