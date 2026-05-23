using AutoMapper;
using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Values.Queries.GetValueById;

public class GetValueByIdQueryHandler : IRequestHandler<GetValueByIdQuery, ValueDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetValueByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ValueDto> Handle(
        GetValueByIdQuery request,
        CancellationToken cancellationToken)
    {
        var value = await _context.Values
            .AsNoTracking()
            .Include(v => v.Property)
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Value), request.Id);

        return _mapper.Map<ValueDto>(value);
    }
}
