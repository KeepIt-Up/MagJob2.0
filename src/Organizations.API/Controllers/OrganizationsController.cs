using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizations.API.Common.Models;
using Organizations.API.Models;
using Organizations.API.Services;

namespace Organizations.API.Controllers;

public record GetOrganizationsByUserIdQuery(string UserId, PaginationOptions Options);

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;
    private readonly ILogger<OrganizationsController> _logger;

    public OrganizationsController(IOrganizationService organizationService, ILogger<OrganizationsController> logger)
    {
        _organizationService = organizationService ?? throw new ArgumentNullException(nameof(organizationService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization(string name, string? description)
    {
        var createdOrganization = await _organizationService.CreateOrganizationAsync(name, description);
        return CreatedAtAction(nameof(GetOrganizationById), new { id = createdOrganization.Id }, createdOrganization);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizationById(int id)
    {
        var organization = await _organizationService.GetByIdAsync(id);
        if (organization == null)
        {
            return NotFound();
        }
        return Ok(organization);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(int id, [FromBody] UpdateOrganizationRequest organization)
    {

        var updatedOrganization = await _organizationService.UpdateOrganizationAsync(id, organization);
        if (updatedOrganization == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(int id)
    {
        var success = await _organizationService.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("invitations")]
    public async Task<IActionResult> GetInvitationsByOrganizationId([FromQuery] GetInvitationsByOrganizationIdQuery query)
    {
        var invitations = await _organizationService.GetInvitationsAsync(query.OrganizationId, query.Options);
        return Ok(invitations);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetOrganizationsByUserIdPaginated([FromQuery] GetOrganizationsByUserIdQuery query)
    {
        var organizations = await _organizationService.GetByUserIdAsync(query.UserId, query.Options);
        return Ok(organizations);
    }

    [HttpPost("users/{userId}/invite")]
    public async Task<IActionResult> InviteUserToOrganization(int organizationId, string userId)
    {
        var invitation = await _organizationService.InviteUserAsync(organizationId, userId);
        return CreatedAtAction(nameof(InviteUserToOrganization), new { id = invitation.OrganizationId }, invitation);
    }

    [HttpGet("members")]
    public async Task<IActionResult> GetMembersByOrganizationId([FromQuery] GetMembersByOrganizationIdQuery query)
    {
        return Ok(await _organizationService.GetMembersAsync(query.OrganizationId, query.Options));
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRolesByOrganizationIdPagination([FromQuery] GetRolesByOrganizationIdQuery query, CancellationToken cancellationToken = default)
    {
        return Ok(await _organizationService.GetRolesAsync(query.OrganizationId, query.Options));
    }
}
