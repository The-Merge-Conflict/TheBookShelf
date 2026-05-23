using DLMS.Application.Features.Values.Commands.AddValue;
using DLMS.Application.Features.Values.Commands.DeleteValue;
using DLMS.Application.Features.Values.Commands.UpdateValue;
using DLMS.Application.Features.Values.Queries.GetValueById;
using DLMS.Application.Features.Values.Queries.GetValuesByResource;
using DLMS.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValueType = DLMS.Domain.Enums.ValueType;

namespace DLMS.API.Controllers;

[Authorize]
public class ValuesController : BaseApiController
{
    /// <summary>Get values by resource Id.</summary>
    [HttpGet("by-resource/{resourceId:int}")]
    public async Task<IActionResult> GetByResource(int resourceId, CancellationToken ct)
        => Ok(await Mediator.Send(new GetValuesByResourceQuery(resourceId), ct));

    /// <summary>Get a value by Id.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
        => Ok(await Mediator.Send(new GetValueByIdQuery(id), ct));

    /// <summary>Add a value to an existing resource.</summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Create(
        AddValueRequest request,
        CancellationToken ct)
    {
        var id = await Mediator.Send(
            new AddValueCommand(
                request.ResourceId,
                request.PropertyId,
                request.ValueText,
                request.ValueUri,
                request.ValueResourceId,
                request.ValueType,
                request.Language),
            ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Update an existing value.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Update(
        int id,
        UpdateValueRequest request,
        CancellationToken ct)
    {
        await Mediator.Send(
            new UpdateValueCommand(
                id,
                request.ValueText,
                request.ValueUri,
                request.ValueResourceId,
                request.ValueType,
                request.Language),
            ct);

        return NoContent();
    }

    /// <summary>Delete a value.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteValueCommand(id), ct);
        return NoContent();
    }
}

public record AddValueRequest(
    int ResourceId,
    int PropertyId,
    string? ValueText,
    string? ValueUri,
    int? ValueResourceId,
    ValueType ValueType,
    LanguageCode? Language);

public record UpdateValueRequest(
    string? ValueText,
    string? ValueUri,
    int? ValueResourceId,
    ValueType ValueType,
    LanguageCode? Language);
