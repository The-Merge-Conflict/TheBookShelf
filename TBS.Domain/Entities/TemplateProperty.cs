namespace TBS.Domain.Entities;

public class TemplateProperty
{
    public int TemplateId { get; private set; }
    public ResourceTemplate Template { get; private set; } = null!;
    public int PropertyId { get; private set; }
    public Property Property { get; private set; } = null!;
    public bool IsRequired { get; private set; }
    public int DisplayOrder { get; private set; }
    public string? AlternateLabel { get; private set; }

    private TemplateProperty() { }

    public static TemplateProperty Create(
        int templateId, int propertyId,
        bool isRequired, int displayOrder,
        string? alternateLabel = null)
    {
        return new TemplateProperty
        {
            TemplateId = templateId,
            PropertyId = propertyId,
            IsRequired = isRequired,
            DisplayOrder = displayOrder,
            AlternateLabel = alternateLabel
        };
    }

    public void Update(bool isRequired, int displayOrder, string? alternateLabel)
    {
        IsRequired = isRequired;
        DisplayOrder = displayOrder;
        AlternateLabel = alternateLabel;
    }
}