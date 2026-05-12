using AutoMapper;
using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Media.Queries.GetMediaByItem;

public class GetMediaByItemQueryHandler
    : IRequestHandler<GetMediaByItemQuery, IReadOnlyList<MediaDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMediaByItemQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<MediaDto>> Handle(
        GetMediaByItemQuery request,
        CancellationToken cancellationToken)
    {
        // Verify the parent Item exists
        var itemExists = await _context.Items
            .AnyAsync(i => i.Id == request.ItemId, cancellationToken);

        if (!itemExists)
            throw new NotFoundException(nameof(Item), request.ItemId);

        var mediaList = await _context.Media
            .AsNoTracking()
            .Where(m => m.ItemId == request.ItemId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<MediaDto>>(mediaList);
    }
}
