using MediatR;

namespace DLMS.Application.Features.ResourceTemplates.Commands.AddPropertyToTemplate;

public record AddPropertyToTemplateCommand(
    int TemplateId,
    int PropertyId,
    bool IsRequired,
    int DisplayOrder,
    string AlternateLabel
) : IRequest<Unit>;