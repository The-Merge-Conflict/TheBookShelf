using DLMS.Application.Common.Models;
using DLMS.Application.Features.Items.Commands.CreateItem;
using DLMS.Application.Features.Items.Commands.DeleteItem;
using DLMS.Application.Features.Items.Commands.UpdateItem;
using DLMS.Application.Features.Items.Queries.GetItemById;
using DLMS.Application.Features.Items.Queries.GetItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Controllers;

[Authorize]
public class ItemsController : BaseApiController
{
    /// <summary>Get a paginated list of items.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? templateId = null,
        CancellationToken ct = default)
        => Ok(await Mediator.Send(new GetItemsQuery(page, pageSize, templateId), ct));

    /// <summary>Get a single item by Id, including its metadata values and media.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
        => Ok(await Mediator.Send(new GetItemByIdQuery(id), ct));

    /// <summary>Create a new item with metadata values.</summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Create(
        CreateItemRequest request,
        CancellationToken ct)
    {
        var id = await Mediator.Send(
            new CreateItemCommand(request.TemplateId, request.Values), ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Replace all metadata values on an existing item.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Update(
        int id,
        UpdateItemRequest request,
        CancellationToken ct)
    {
        await Mediator.Send(
            new UpdateItemCommand(id, request.TemplateId, request.Values), ct);

        return NoContent();
    }

    /// <summary>Delete an item and all its associated values.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteItemCommand(id), ct);
        return NoContent();
    }
}

public record CreateItemRequest(int? TemplateId, List<ValueInput> Values);
public record UpdateItemRequest(int? TemplateId, List<ValueInput> Values);