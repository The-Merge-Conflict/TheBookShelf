using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Values.Commands.DeleteValue;

public class DeleteValueCommandHandler : IRequestHandler<DeleteValueCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteValueCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        DeleteValueCommand request,
        CancellationToken cancellationToken)
    {
        var value = await _context.Values
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Value), request.Id);

        _context.Values.Remove(value);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
