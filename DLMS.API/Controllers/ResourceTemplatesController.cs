using DLMS.Application.Features.ResourceTemplates.Commands.AddPropertyToTemplate;
using DLMS.Application.Features.ResourceTemplates.Commands.CreateResourceTemplate;
using DLMS.Application.Features.ResourceTemplates.Commands.DeleteResourceTemplate;
using DLMS.Application.Features.ResourceTemplates.Commands.RemovePropertyFromTemplate;
using DLMS.Application.Features.ResourceTemplates.Commands.UpdateResourceTemplate;
using DLMS.Application.Features.ResourceTemplates.Queries.GetAllResourceTemplates;
using DLMS.Application.Features.ResourceTemplates.Queries.GetResourceTemplateById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Controllers;

[Authorize]
public class ResourceTemplatesController : BaseApiController
{
    /// <summary>Get all resource templates.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await Mediator.Send(new GetAllResourceTemplatesQuery(), ct));

    /// <summary>Get a resource template by Id (includes its properties).</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
        => Ok(await Mediator.Send(new GetResourceTemplateByIdQuery(id), ct));

    /// <summary>Create a new resource template.</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
        CreateResourceTemplateCommand command,
        CancellationToken ct)
    {
        var id = await Mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Update a resource template's label and description.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        int id,
        UpdateResourceTemplateRequest request,
        CancellationToken ct)
    {
        await Mediator.Send(
            new UpdateResourceTemplateCommand(id, request.Label, request.Description),
            ct);

        return NoContent();
    }

    /// <summary>Delete a resource template.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteResourceTemplateCommand(id), ct);
        return NoContent();
    }

    /// <summary>Add a property to a template.</summary>
    [HttpPost("{id:int}/properties")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddProperty(
        int id,
        AddPropertyRequest request,
        CancellationToken ct)
    {
        await Mediator.Send(
            new AddPropertyToTemplateCommand(
                id,
                request.PropertyId,
                request.IsRequired,
                request.DisplayOrder,
                request.AlternateLabel ?? string.Empty),
            ct);

        return NoContent();
    }

    /// <summary>Remove a property from a template.</summary>
    [HttpDelete("{id:int}/properties/{propertyId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveProperty(
        int id, int propertyId,
        CancellationToken ct)
    {
        await Mediator.Send(new RemovePropertyFromTemplateCommand(id, propertyId), ct);
        return NoContent();
    }
}

public record UpdateResourceTemplateRequest(string Label, string Description);
public record AddPropertyRequest(
    int PropertyId,
    bool IsRequired,
    int DisplayOrder,
    string? AlternateLabel);