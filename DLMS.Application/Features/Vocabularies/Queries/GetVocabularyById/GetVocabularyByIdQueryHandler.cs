using AutoMapper;
using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Application.DTOs;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Vocabularies.Queries.GetVocabularyById;

public class GetVocabularyByIdQueryHandler
    : IRequestHandler<GetVocabularyByIdQuery, VocabularyDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVocabularyByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<VocabularyDto> Handle(
        GetVocabularyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var vocabulary = await _context.Vocabularies
            .AsNoTracking()
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Vocabulary), request.Id);

        return _mapper.Map<VocabularyDto>(vocabulary);
    }
}