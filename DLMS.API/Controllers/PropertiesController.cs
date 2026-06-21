using DLMS.Application.Features.Properties.Commands.CreateProperty;
using DLMS.Application.Features.Properties.Commands.DeleteProperty;
using DLMS.Application.Features.Properties.Commands.UpdateProperty;
using DLMS.Application.Features.Properties.Queries.GetAllProperties;
using DLMS.Application.Features.Properties.Queries.GetPropertiesByVocabulary;
using DLMS.Application.Features.Properties.Queries.GetPropertyById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Controllers;

[Authorize]
public class PropertiesController : BaseApiController
{
    /// <summary>Get all properties across all vocabularies.</summary>
    /// Get a paginated, searchable list of properties across all vocabularies.
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        CancellationToken ct = default)
        => Ok(await Mediator.Send(new GetAllPropertiesQuery(page, pageSize, search), ct));

    /// <summary>Get all properties belonging to a specific vocabulary.</summary>
    [HttpGet("by-vocabulary/{vocabularyId:int}")]
    public async Task<IActionResult> GetByVocabulary(int vocabularyId, CancellationToken ct)
        => Ok(await Mediator.Send(new GetPropertiesByVocabularyQuery(vocabularyId), ct));

    /// <summary>Get a property by Id.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
        => Ok(await Mediator.Send(new GetPropertyByIdQuery(id), ct));

    /// <summary>Create a new property inside a vocabulary.</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
        CreatePropertyCommand command,
        CancellationToken ct)
    {
        var id = await Mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Update an existing property.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePropertyRequest request,
        CancellationToken ct)
    {
        await Mediator.Send(
            new UpdatePropertyCommand(id, request.LocalName, request.Label, request.TermUri),
            ct);

        return NoContent();
    }

    /// <summary>Delete a property.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Mediator.Send(new DeletePropertyCommand(id), ct);
        return NoContent();
    }
}

public record UpdatePropertyRequest(string LocalName, string Label, string TermUri);