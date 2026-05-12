using DLMS.Application.Features.ItemSets.Commands.AddItemToSet;
using DLMS.Application.Features.ItemSets.Commands.CreateItemSet;
using DLMS.Application.Features.ItemSets.Commands.DeleteItemSet;
using DLMS.Application.Features.ItemSets.Commands.RemoveItemFromSet;
using DLMS.Application.Features.ItemSets.Commands.UpdateItemSet;
using DLMS.Application.Features.ItemSets.Queries.GetAllItemSets;
using DLMS.Application.Features.ItemSets.Queries.GetItemSetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Controllers;

[Authorize]
public class ItemSetsController : BaseApiController
{
    /// <summary>Get all item sets.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await Mediator.Send(new GetAllItemSetsQuery(), ct));

    /// <summary>Get an item set by Id.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
        => Ok(await Mediator.Send(new GetItemSetByIdQuery(id), ct));

    /// <summary>Create a new item set (collection).</summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Create(
        CreateItemSetCommand command,
        CancellationToken ct)
    {
        var id = await Mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Update an item set's title, description, and visibility.</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Update(
        int id,
        UpdateItemSetRequest request,
        CancellationToken ct)
    {
        await Mediator.Send(
            new UpdateItemSetCommand(id, request.Title, request.Description, request.IsPublic), ct);

        return NoContent();
    }

    /// <summary>Delete an item set.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteItemSetCommand(id), ct);
        return NoContent();
    }

    /// <summary>Add an item to this set.</summary>
    [HttpPost("{id:int}/items/{itemId:int}")]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> AddItem(int id, int itemId, CancellationToken ct)
    {
        await Mediator.Send(new AddItemToSetCommand(id, itemId), ct);
        return NoContent();
    }

    /// <summary>Remove an item from this set.</summary>
    [HttpDelete("{id:int}/items/{itemId:int}")]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> RemoveItem(int id, int itemId, CancellationToken ct)
    {
        await Mediator.Send(new RemoveItemFromSetCommand(id, itemId), ct);
        return NoContent();
    }
}

public record UpdateItemSetRequest(string Title, string Description, bool IsPublic);