using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ResourceTemplates.Commands.UpdateTemplateProperty;

public class UpdateTemplatePropertyCommandHandler
    : IRequestHandler<UpdateTemplatePropertyCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTemplatePropertyCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        UpdateTemplatePropertyCommand request,
        CancellationToken cancellationToken)
    {
        var link = await _context.TemplateProperties
            .FirstOrDefaultAsync(tp =>
                tp.TemplateId == request.TemplateId &&
                tp.PropertyId == request.PropertyId,
                cancellationToken)
            ?? throw new NotFoundException(
                $"{nameof(TemplateProperty)} (Template {request.TemplateId}, Property {request.PropertyId})",
                $"{request.TemplateId}-{request.PropertyId}");

        link.IsRequired = request.IsRequired;
        link.DisplayOrder = request.DisplayOrder;
        link.AlternateLabel = request.AlternateLabel;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
