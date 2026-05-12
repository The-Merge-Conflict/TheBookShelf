using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ItemSets.Commands.AddItemToSet;

public class AddItemToSetCommandHandler : IRequestHandler<AddItemToSetCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public AddItemToSetCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        AddItemToSetCommand request,
        CancellationToken cancellationToken)
    {
        var itemSet = await _context.ItemSets
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == request.ItemSetId, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemSet), request.ItemSetId);

        var item = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(Item), request.ItemId);

        // Idempotent — skip if already linked
        if (!itemSet.Items.Any(i => i.Id == request.ItemId))
        {
            itemSet.Items.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}