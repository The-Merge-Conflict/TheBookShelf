using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Commands.UpdateTemplateProperty;

public record UpdateTemplatePropertyCommand(
    int TemplateId,
    int PropertyId,
    bool IsRequired,
    int DisplayOrder,
    string? AlternateLabel
) : IRequest<Unit>;
