using DLMS.Application.Common.Exceptions;
using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DLMS.Application.Features.Vocabularies.Commands.DeleteVocabulary;

public class DeleteVocabularyCommandHandler : IRequestHandler<DeleteVocabularyCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteVocabularyCommandHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Unit> Handle(
        DeleteVocabularyCommand request,
        CancellationToken cancellationToken)
    {
        var vocabulary = await _context.Vocabularies
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Vocabulary), request.Id);

        _context.Vocabularies.Remove(vocabulary);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}