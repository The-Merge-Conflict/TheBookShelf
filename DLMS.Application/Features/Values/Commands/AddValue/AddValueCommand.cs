using DLMS.Domain.ValueObjects;
using MediatR;
using ValueType = DLMS.Domain.Enums.ValueType;

namespace DLMS.Application.Features.Values.Commands.AddValue;

public record AddValueCommand(
    int ResourceId,
    int PropertyId,
    string? ValueText,
    string? ValueUri,
    int? ValueResourceId,
    ValueType ValueType,
    LanguageCode? Language
) : IRequest<int>;
