using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Values.Commands.UpdateValue;

public class UpdateValueCommandHandler : IRequestHandler<UpdateValueCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateValueCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        UpdateValueCommand request,
        CancellationToken cancellationToken)
    {
        var value = await _context.Values
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Value), request.Id);

        value.ValueText = request.ValueText;
        value.ValueUri = request.ValueUri;
        value.ValueResourceId = request.ValueResourceId;
        value.ValueType = request.ValueType;
        value.Language = request.Language;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
