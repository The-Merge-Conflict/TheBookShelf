using AutoMapper;
using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Properties.Queries.GetPropertyById;

public class GetPropertyByIdQueryHandler
    : IRequestHandler<GetPropertyByIdQuery, PropertyDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPropertyByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PropertyDto> Handle(
        GetPropertyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var property = await _context.Properties
            .AsNoTracking()
            .Include(p => p.Vocabulary)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Property), request.Id);

        return _mapper.Map<PropertyDto>(property);
    }
}