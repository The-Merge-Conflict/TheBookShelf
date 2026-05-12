using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ItemSets.Queries.GetAllItemSets;

public class GetAllItemSetsQueryHandler
    : IRequestHandler<GetAllItemSetsQuery, List<ItemSetDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllItemSetsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ItemSetDto>> Handle(
        GetAllItemSetsQuery request,
        CancellationToken cancellationToken)
        => await _context.ItemSets
            .AsNoTracking()
            .OrderBy(s => s.Title)
            .ProjectTo<ItemSetDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}