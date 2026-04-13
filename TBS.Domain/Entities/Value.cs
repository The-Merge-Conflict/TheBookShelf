using TBS.Domain.Enums;
using TBS.Domain.ValueObjects;

namespace TBS.Domain.Entities;

public class Value
{
    public int Id { get; private set; }
    public int ResourceId { get; private set; }
    public Resource Resource { get; private set; } = null!;
    public int PropertyId { get; private set; }
    public Property Property { get; private set; } = null!;

    public string? ValueText { get; private set; }
    public string? ValueUri { get; private set; }
    public int? ValueResourceId { get; private set; }    // FK → Resource (internal linking)
    public Resource? ValueResource { get; private set; }

    public ValueType Type { get; private set; }
    public Language? Language { get; private set; }      // null for uri/resource types

    private Value() { }

    public static Value CreateLiteral(int resourceId, int propertyId, string text, string? languageCode = null)
    {
        return new Value
        {
            ResourceId = resourceId,
            PropertyId = propertyId,
            ValueText = text,
            Type = ValueType.Literal,
            Language = languageCode is not null ? Language.Create(languageCode) : null
        };
    }

    public static Value CreateUri(int resourceId, int propertyId, string uri)
    {
        return new Value
        {
            ResourceId = resourceId,
            PropertyId = propertyId,
            ValueUri = uri,
            Type = ValueType.Uri
        };
    }

    public static Value CreateResourceLink(int resourceId, int propertyId, int valueResourceId)
    {
        return new Value
        {
            ResourceId = resourceId,
            PropertyId = propertyId,
            ValueResourceId = valueResourceId,
            Type = ValueType.Resource
        };
    }
}