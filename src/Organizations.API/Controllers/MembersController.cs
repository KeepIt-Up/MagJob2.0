using Organizations.Application.Features.Members.Delete;
using Organizations.Application.Features.Members.Get;
using Organizations.Application.Features.Members.Update;

namespace Organizations.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Get a member by its id
    /// </summary>
    /// <param name="id"> The id of the member </param>
    /// <returns> The member </returns>
    /// <response code="200"> The member </response>
    /// <response code="404"> The member was not found </response>
    /// <response code="401"> The user is not authorized to access this member </response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember(Guid id)
    {
        return Ok(await _mediator.Send(new GetMemberRequest(id)));
    }

    /// <summary>
    /// Update a member by its id
    /// </summary>
    /// <param name="id"> The id of the member </param>
    /// <param name="request"> The request object containing the member details </param>
    /// <returns> The updated member </returns>
    /// <response code="200"> The updated member </response>
    /// <response code="404"> The member was not found </response>
    /// <response code="401"> The user is not authorized to access this member </response>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(Guid id, UpdateMemberRequest request)
    {
        return Ok(await _mediator.Send(new UpdateMemberRequest(id, request.firstName, request.lastName, request.notes)));
    }

    /// <summary>
    /// Delete a member by its id
    /// </summary>
    /// <param name="id"> The id of the member </param>
    /// <returns> No content </returns>
    /// <response code="204"> The member was deleted </response>
    /// <response code="404"> The member was not found </response>
    /// <response code="401"> The user is not authorized to access this member </response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(Guid id)
    {
        await _mediator.Send(new DeleteMemberRequest(id));
        return NoContent();
    }
}