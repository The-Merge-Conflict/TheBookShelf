using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Values.Queries.GetValuesByResource;

public class GetValuesByResourceQueryHandler
    : IRequestHandler<GetValuesByResourceQuery, List<ValueDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetValuesByResourceQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ValueDto>> Handle(
        GetValuesByResourceQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Values
            .AsNoTracking()
            .Include(v => v.Property)
            .Where(v => v.ResourceId == request.ResourceId)
            .ProjectTo<ValueDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
