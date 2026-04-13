using TBS.Domain.ValueObjects;

namespace TBS.Domain.Entities;

public class Property
{
    public int Id { get; private set; }
    public int VocabularyId { get; private set; }
    public Vocabulary Vocabulary { get; private set; } = null!;
    public string LocalName { get; private set; } = string.Empty; // e.g. "title", "creator"
    public string Label { get; private set; } = string.Empty;     // human-friendly
    public TermUri TermUri { get; private set; } = null!;         // full RDF URI

    private readonly List<TemplateProperty> _templateProperties = [];
    public IReadOnlyCollection<TemplateProperty> TemplateProperties => _templateProperties.AsReadOnly();

    private Property() { }

    public static Property Create(int vocabularyId, string localName, string label, string termUri)
    {
        return new Property
        {
            VocabularyId = vocabularyId,
            LocalName = localName,
            Label = label,
            TermUri = TermUri.Create(termUri)
        };
    }

    public void Update(string localName, string label, string termUri)
    {
        LocalName = localName;
        Label = label;
        TermUri = TermUri.Create(termUri);
    }
}