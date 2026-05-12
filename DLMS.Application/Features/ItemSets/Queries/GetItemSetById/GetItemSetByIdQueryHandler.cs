using AutoMapper;
using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.ItemSets.Queries.GetItemSetById;

public class GetItemSetByIdQueryHandler : IRequestHandler<GetItemSetByIdQuery, ItemSetDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemSetByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ItemSetDto> Handle(
        GetItemSetByIdQuery request,
        CancellationToken cancellationToken)
    {
        var itemSet = await _context.ItemSets
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(ItemSet), request.Id);

        return _mapper.Map<ItemSetDto>(itemSet);
    }
}