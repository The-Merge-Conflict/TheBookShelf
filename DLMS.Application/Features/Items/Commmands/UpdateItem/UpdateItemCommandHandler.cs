using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Items.Commands.UpdateItem;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateItemCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(
        UpdateItemCommand request,
        CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .Include(i => i.Values)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Item), request.Id);

        if (request.TemplateId.HasValue)
        {
            var templateExists = await _context.ResourceTemplates
                .AnyAsync(t => t.Id == request.TemplateId.Value, cancellationToken);

            if (!templateExists)
                throw new NotFoundException(nameof(ResourceTemplate), request.TemplateId.Value);
        }

        // Replace all values
        _context.Values.RemoveRange(item.Values);

        item.TemplateId = request.TemplateId;
        item.ModifiedAt = DateTime.UtcNow;
        item.ModifiedBy = _currentUser.UserName ?? "system";

        foreach (var v in request.Values)
        {
            item.Values.Add(new Value
            {
                PropertyId = v.PropertyId,
                ValueText = v.ValueText,
                ValueUri = v.ValueUri,
                ValueResourceId = v.ValueResourceId,
                ValueType = v.Type,
                Language = v.Language
            });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}