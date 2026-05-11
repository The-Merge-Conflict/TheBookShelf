using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Vocabularies.Commands.UpdateVocabulary;

public class UpdateVocabularyCommandHandler : IRequestHandler<UpdateVocabularyCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateVocabularyCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        UpdateVocabularyCommand request,
        CancellationToken cancellationToken)
    {
        var vocabulary = await _context.Vocabularies
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Vocabulary), request.Id);

        vocabulary.Prefix = request.Prefix;
        vocabulary.NamespaceUri = request.NamespaceUri;
        vocabulary.Label = request.Label;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}