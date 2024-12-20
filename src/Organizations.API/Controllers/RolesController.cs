using Microsoft.AspNetCore.Mvc;
using Organizations.API.Common.Models;

namespace Organizations.API.Controllers;

public record GetRolesByOrganizationIdQuery(int OrganizationId) : QueryWithPaginationOptions;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRolePayload payload, CancellationToken cancellationToken = default)
    {
        return Ok(await _roleService.CreateRole(payload.Name, payload.OrganizationId, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        return Ok(await _roleService.GetByIdAsync(id, cancellationToken));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRolePayload payload, CancellationToken cancellationToken = default)
    {
        return Ok(await _roleService.UpdateRole(id, payload, cancellationToken));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        return Ok(await _roleService.DeleteAsync(id, cancellationToken));
    }

    [HttpPost("{id}/permissions")]
    public async Task<IActionResult> AddPermissionsToRole(int id, List<int> permissionIds, CancellationToken cancellationToken = default)
    {
        return Ok(await _roleService.AddPermissionsToRole(id, permissionIds, cancellationToken));
    }

    [HttpDelete("{id}/permissions")]
    public async Task<IActionResult> RemovePermissionsFromRole(int id, List<int> permissionIds, CancellationToken cancellationToken = default)
    {
        return Ok(await _roleService.RemovePermissionsFromRole(id, permissionIds, cancellationToken));
    }

    [HttpPost("{id}/members")]
    public async Task<IActionResult> AddMembersToRole(int id, List<int> memberIds, CancellationToken cancellationToken = default)
    {
        return Ok(await _roleService.AddMembersToRole(id, memberIds, cancellationToken));
    }

    [HttpDelete("{id}/members")]
    public async Task<IActionResult> RemoveMembersFromRole(int id, List<int> memberIds, CancellationToken cancellationToken = default)
    {
        return Ok(await _roleService.RemoveMembersFromRole(id, memberIds, cancellationToken));
    }
}