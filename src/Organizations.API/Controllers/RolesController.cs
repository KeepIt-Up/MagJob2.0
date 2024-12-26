using Microsoft.AspNetCore.Mvc;
using Organizations.Application.Features.Roles.Create;
using Organizations.Application.Features.Roles.Get;
using Organizations.Application.Features.Roles.Update;

namespace Organizations.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Create a role
    /// </summary>
    /// <param name="request"> The request object containing the role name </param>
    /// <returns> The created role </returns>
    /// <response code="200"> The created role </response>
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleRequest request)
    {
        return Ok(await mediator.Send(request));
    }

    /// <summary>
    /// Get a role by id
    /// </summary>
    /// <param name="request"> The request object containing the role id </param>
    /// <returns> The role </returns>
    /// <response code="200"> The role </response>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(GetRoleRequest request)
    {
        return Ok(await mediator.Send(request));
    }

    /// <summary>
    /// Update a role
    /// </summary>
    /// <param name="request"> The request object containing the role id and the role name </param>
    /// <returns> The updated role </returns>
    /// <response code="200"> The updated role </response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UpdateRoleRequest request)
    {
        return Ok(await mediator.Send(request));
    }

    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="request"> The request object containing the role id </param>
    /// <returns> The deleted role </returns>
    /// <response code="200"> The deleted role </response>
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(DeleteRoleRequest request)
    // {
    //     await _mediator.Send(request);
    //     return NoContent();
    // }

    // [HttpPost("{id}/permissions")]
    // public async Task<IActionResult> AddPermissionsToRole(AddPermissionsToRoleRequest request)
    // {
    //     return Ok(await _mediator.Send(request));
    // }

    // [HttpDelete("{id}/permissions")]
    // public async Task<IActionResult> RemovePermissionsFromRole(RemovePermissionsFromRoleRequest request)
    // {
    //     return Ok(await _mediator.Send(request));
    // }

    // [HttpPost("{id}/members")]
    // public async Task<IActionResult> AddMembersToRole(AddMembersToRoleRequest request)
    // {
    //     return Ok(await _mediator.Send(request));
    // }

    // [HttpDelete("{id}/members")]
    // public async Task<IActionResult> RemoveMembersFromRole(RemoveMembersFromRoleRequest request)
    // {
    //     return Ok(await _mediator.Send(request));
    // }
}