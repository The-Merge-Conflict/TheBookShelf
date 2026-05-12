using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.Common.Models;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Items.Queries.GetItems;

public class GetItemsQueryHandler
    : IRequestHandler<GetItemsQuery, PaginatedResult<ItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<ItemDto>> Handle(
        GetItemsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Items
            .AsNoTracking()
            .Include(i => i.Template)
            .Include(i => i.Values).ThenInclude(v => v.Property)
            .Include(i => i.MediaList)
            .AsQueryable();

        if (request.TemplateId.HasValue)
            query = query.Where(i => i.TemplateId == request.TemplateId);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(i => i.CreatedAt)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ItemDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}