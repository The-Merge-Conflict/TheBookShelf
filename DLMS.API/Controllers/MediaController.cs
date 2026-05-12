using DLMS.Application.Features.Media.Commands.DeleteMedia;
using DLMS.Application.Features.Media.Commands.UploadMedia;
using DLMS.Application.Features.Media.Queries.GetMediaByItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DLMS.API.Controllers;

[Authorize]
public class MediaController : BaseApiController
{
    /// <summary>Get all media files belonging to a specific item.</summary>
    [HttpGet("by-item/{itemId:int}")]
    public async Task<IActionResult> GetByItem(int itemId, CancellationToken ct)
        => Ok(await Mediator.Send(new GetMediaByItemQuery(itemId), ct));

    /// <summary>Upload a media file and attach it to an item.</summary>
    [HttpPost]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Upload(
        [FromForm] int itemId,
        [FromForm] string? altText,
        IFormFile file,
        CancellationToken ct)
    {
        var command = new UploadMediaCommand(itemId, file, altText);
        var id = await Mediator.Send(command, ct);
        return CreatedAtAction(nameof(GetByItem), new { itemId }, new { id });
    }

    /// <summary>Delete a media file by its Id.</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await Mediator.Send(new DeleteMediaCommand(id), ct);
        return NoContent();
    }
}
