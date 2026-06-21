using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.Common.Models;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Properties.Queries.GetAllProperties;

public class GetAllPropertiesQueryHandler
    : IRequestHandler<GetAllPropertiesQuery, PaginatedResult<PropertyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllPropertiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedResult<PropertyDto>> Handle(
        GetAllPropertiesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Properties
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim();
            query = query.Where(p =>
                p.LocalName.Contains(term) ||
                p.Label.Contains(term) ||
                p.TermUri.Contains(term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(p => p.Label)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<PropertyDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}