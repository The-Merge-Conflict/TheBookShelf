using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Items.Commands.CreateItem;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateItemCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(
        CreateItemCommand request,
        CancellationToken cancellationToken)
    {
        if (request.TemplateId.HasValue)
        {
            var templateExists = await _context.ResourceTemplates
                .AnyAsync(t => t.Id == request.TemplateId.Value, cancellationToken);

            if (!templateExists)
                throw new NotFoundException(nameof(ResourceTemplate), request.TemplateId.Value);
        }

        var item = new Item
        {
            TemplateId = request.TemplateId,
            OwnerId = _currentUser.UserId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = _currentUser.UserName ?? "system"
        };

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

        _context.Items.Add(item);
        await _context.SaveChangesAsync(cancellationToken);

        return item.Id;
    }
}