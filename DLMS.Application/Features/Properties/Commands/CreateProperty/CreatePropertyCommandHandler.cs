using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Properties.Commands.CreateProperty;

public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreatePropertyCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<int> Handle(
        CreatePropertyCommand request,
        CancellationToken cancellationToken)
    {
        var vocabularyExists = await _context.Vocabularies
            .AnyAsync(v => v.Id == request.VocabularyId, cancellationToken);

        if (!vocabularyExists)
            throw new NotFoundException(nameof(Vocabulary), request.VocabularyId);

        var property = new Property
        {
            VocabularyId = request.VocabularyId,
            LocalName = request.LocalName,
            Label = request.Label,
            TermUri = request.TermUri
        };

        _context.Properties.Add(property);
        await _context.SaveChangesAsync(cancellationToken);

        return property.Id;
    }
}