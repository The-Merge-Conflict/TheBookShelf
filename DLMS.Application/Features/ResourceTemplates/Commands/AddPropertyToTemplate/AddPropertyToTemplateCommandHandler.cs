using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ResourceTemplates.Commands.AddPropertyToTemplate;

public class AddPropertyToTemplateCommandHandler
    : IRequestHandler<AddPropertyToTemplateCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public AddPropertyToTemplateCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        AddPropertyToTemplateCommand request,
        CancellationToken cancellationToken)
    {
        var templateExists = await _context.ResourceTemplates
            .AnyAsync(t => t.Id == request.TemplateId, cancellationToken);
        if (!templateExists)
            throw new NotFoundException(nameof(ResourceTemplate), request.TemplateId);

        var propertyExists = await _context.Properties
            .AnyAsync(p => p.Id == request.PropertyId, cancellationToken);
        if (!propertyExists)
            throw new NotFoundException(nameof(Property), request.PropertyId);

        var alreadyLinked = await _context.TemplateProperties
            .AnyAsync(tp =>
                tp.TemplateId == request.TemplateId &&
                tp.PropertyId == request.PropertyId,
                cancellationToken);

        if (alreadyLinked)
            return Unit.Value;

        var templateProperty = new TemplateProperty
        {
            TemplateId = request.TemplateId,
            PropertyId = request.PropertyId,
            IsRequired = request.IsRequired,
            DisplayOrder = request.DisplayOrder,
            AlternateLabel = request.AlternateLabel
        };

        _context.TemplateProperties.Add(templateProperty);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}