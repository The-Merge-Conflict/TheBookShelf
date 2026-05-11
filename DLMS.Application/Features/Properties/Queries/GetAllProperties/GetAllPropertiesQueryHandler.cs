using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Properties.Queries.GetAllProperties;

public class GetAllPropertiesQueryHandler
    : IRequestHandler<GetAllPropertiesQuery, List<PropertyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllPropertiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PropertyDto>> Handle(
        GetAllPropertiesQuery request,
        CancellationToken cancellationToken)
        => await _context.Properties
            .AsNoTracking()
            .Include(p => p.Vocabulary)
            .OrderBy(p => p.Label)
            .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}