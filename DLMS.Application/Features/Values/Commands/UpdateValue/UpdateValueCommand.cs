using DLMS.Domain.ValueObjects;
using MediatR;
using ValueType = DLMS.Domain.Enums.ValueType;

namespace DLMS.Application.Features.Values.Commands.UpdateValue;

public record UpdateValueCommand(
    int Id,
    string? ValueText,
    string? ValueUri,
    int? ValueResourceId,
    ValueType ValueType,
    LanguageCode? Language
) : IRequest<Unit>;
