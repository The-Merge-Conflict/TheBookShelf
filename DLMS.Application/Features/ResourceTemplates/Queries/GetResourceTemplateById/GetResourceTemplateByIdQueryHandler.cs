using AutoMapper;
using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ResourceTemplates.Queries.GetResourceTemplateById;

public class GetResourceTemplateByIdQueryHandler
    : IRequestHandler<GetResourceTemplateByIdQuery, ResourceTemplateDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetResourceTemplateByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResourceTemplateDto> Handle(
        GetResourceTemplateByIdQuery request,
        CancellationToken cancellationToken)
    {
        var template = await _context.ResourceTemplates
            .AsNoTracking()
            .Include(t => t.TemplateProperties)
                .ThenInclude(tp => tp.Property)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ResourceTemplate), request.Id);

        return _mapper.Map<ResourceTemplateDto>(template);
    }
}