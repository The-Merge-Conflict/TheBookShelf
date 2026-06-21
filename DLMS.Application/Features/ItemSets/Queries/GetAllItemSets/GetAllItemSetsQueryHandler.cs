using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.Common.Models;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ItemSets.Queries.GetAllItemSets;

public class GetAllItemSetsQueryHandler
    : IRequestHandler<GetAllItemSetsQuery, PaginatedResult<ItemSetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllItemSetsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<ItemSetDto>> Handle(
        GetAllItemSetsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.ItemSets
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim();
            query = query.Where(s =>
                s.Title.Contains(term) ||
                (s.Description != null && s.Description.Contains(term)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(s => s.Title)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<ItemSetDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<ItemSetDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}