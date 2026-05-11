using AutoMapper;
using AutoMapper.QueryableExtensions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Vocabularies.Queries.GetAllVocabularies;

public class GetAllVocabulariesQueryHandler
    : IRequestHandler<GetAllVocabulariesQuery, List<VocabularyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllVocabulariesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<VocabularyDto>> Handle(
        GetAllVocabulariesQuery request,
        CancellationToken cancellationToken)
        => await _context.Vocabularies
            .AsNoTracking()
            .OrderBy(v => v.Label)
            .ProjectTo<VocabularyDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}