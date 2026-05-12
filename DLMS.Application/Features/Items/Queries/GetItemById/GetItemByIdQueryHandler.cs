using AutoMapper;
using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Items.Queries.GetItemById;

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ItemDto> Handle(
        GetItemByIdQuery request,
        CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .AsNoTracking()
            .Include(i => i.Template)
            .Include(i => i.Values)
                .ThenInclude(v => v.Property)
            .Include(i => i.MediaList)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Item), request.Id);

        return _mapper.Map<ItemDto>(item);
    }
}