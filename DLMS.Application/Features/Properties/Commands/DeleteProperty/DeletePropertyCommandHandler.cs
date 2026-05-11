using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Properties.Commands.DeleteProperty;

public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeletePropertyCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        DeletePropertyCommand request,
        CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Property), request.Id);

        _context.Properties.Remove(property);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}