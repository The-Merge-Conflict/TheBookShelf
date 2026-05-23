using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Values.Commands.AddValue;

public class AddValueCommandHandler : IRequestHandler<AddValueCommand, int>
{
    private readonly IApplicationDbContext _context;

    public AddValueCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<int> Handle(
        AddValueCommand request,
        CancellationToken cancellationToken)
    {
        var resourceExists = await _context.Resources
            .AnyAsync(r => r.Id == request.ResourceId, cancellationToken);
        if (!resourceExists)
            throw new NotFoundException(nameof(Resource), request.ResourceId);

        var propertyExists = await _context.Properties
            .AnyAsync(p => p.Id == request.PropertyId, cancellationToken);
        if (!propertyExists)
            throw new NotFoundException(nameof(Property), request.PropertyId);

        var value = new Value
        {
            ResourceId = request.ResourceId,
            PropertyId = request.PropertyId,
            ValueText = request.ValueText,
            ValueUri = request.ValueUri,
            ValueResourceId = request.ValueResourceId,
            ValueType = request.ValueType,
            Language = request.Language
        };

        _context.Values.Add(value);
        await _context.SaveChangesAsync(cancellationToken);

        return value.Id;
    }
}
