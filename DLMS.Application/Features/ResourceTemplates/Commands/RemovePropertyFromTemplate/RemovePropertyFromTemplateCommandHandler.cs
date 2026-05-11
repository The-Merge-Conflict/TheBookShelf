using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ResourceTemplates.Commands.RemovePropertyFromTemplate;

public class RemovePropertyFromTemplateCommandHandler
    : IRequestHandler<RemovePropertyFromTemplateCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public RemovePropertyFromTemplateCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        RemovePropertyFromTemplateCommand request,
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

        _context.TemplateProperties.Remove(link);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}