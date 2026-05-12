using DLMS.Application.Features.Vocabularies.Commands.CreateVocabulary;
using DLMS.Application.Features.Vocabularies.Commands.DeleteVocabulary;
using DLMS.Application.Features.Vocabularies.Commands.UpdateVocabulary;
using DLMS.Application.Features.Vocabularies.Queries.GetAllVocabularies;
using DLMS.Application.Features.Vocabularies.Queries.GetVocabularyById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Controllers;

[Authorize]
public class VocabulariesController : BaseApiController
{
    /// <summary>Get all vocabularies.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await Mediator.Send(new GetAllVocabulariesQuery(), ct));

    /// <summary>Get a vocabulary by Id.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
        => Ok(await Mediator.Send(new GetVocabularyByIdQuery(id), ct));

    /// <summary>Create a new vocabulary.</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
        CreateVocabularyCommand command,
        CancellationToken ct)
    {
        var id = await Mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Update an existing vocabulary.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        int id,
        UpdateVocabularyRequest request,
        CancellationToken ct)
    {
        await Mediator.Send(
            new UpdateVocabularyCommand(id, request.Prefix, request.NamespaceUri, request.Label),
            ct);

        return NoContent();
    }

    /// <summary>Delete a vocabulary.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteVocabularyCommand(id), ct);
        return NoContent();
    }
}

public record UpdateVocabularyRequest(string Prefix, string NamespaceUri, string Label);