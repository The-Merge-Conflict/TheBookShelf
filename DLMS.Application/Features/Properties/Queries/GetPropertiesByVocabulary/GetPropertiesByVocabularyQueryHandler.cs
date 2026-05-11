using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Properties.Queries.GetPropertiesByVocabulary;

public class GetPropertiesByVocabularyQueryHandler
    : IRequestHandler<GetPropertiesByVocabularyQuery, List<PropertyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPropertiesByVocabularyQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PropertyDto>> Handle(
        GetPropertiesByVocabularyQuery request,
        CancellationToken cancellationToken)
        => await _context.Properties
            .AsNoTracking()
            .Include(p => p.Vocabulary)
            .Where(p => p.VocabularyId == request.VocabularyId)
            .OrderBy(p => p.LocalName)
            .ProjectTo<PropertyDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}