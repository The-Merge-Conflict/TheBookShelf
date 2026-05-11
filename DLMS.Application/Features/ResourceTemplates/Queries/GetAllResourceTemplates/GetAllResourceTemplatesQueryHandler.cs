using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ResourceTemplates.Queries.GetAllResourceTemplates;

public class GetAllResourceTemplatesQueryHandler
    : IRequestHandler<GetAllResourceTemplatesQuery, List<ResourceTemplateDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllResourceTemplatesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ResourceTemplateDto>> Handle(
        GetAllResourceTemplatesQuery request,
        CancellationToken cancellationToken)
        => await _context.ResourceTemplates
            .AsNoTracking()
            .Include(t => t.TemplateProperties)
                .ThenInclude(tp => tp.Property)
            .OrderBy(t => t.Label)
            .ProjectTo<ResourceTemplateDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}