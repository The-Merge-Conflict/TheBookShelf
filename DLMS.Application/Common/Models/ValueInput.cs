
using DLMS.Domain.ValueObjects;
namespace DLMS.Application.Common.Models;

/// <summary>
/// Shared input model used by CreateItemCommand and UpdateItemCommand
/// to submit metadata values for an Item.
/// Type must be one of: "literal", "uri", "resource"
/// </summary>
public record ValueInput(
    int PropertyId,
    string? ValueText,
    string? ValueUri,
    int? ValueResourceId,
    Domain.Enums.ValueType Type,
    LanguageCode Language
);