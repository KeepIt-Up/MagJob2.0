using Microsoft.AspNetCore.Mvc;
using Organizations.API.Common.Models;
using Organizations.API.Models;
using Organizations.API.Services;

namespace Organizations.API.Controllers;

public record GetInvitationsByOrganizationIdQuery(int OrganizationId) : QueryWithPaginationOptions;

[ApiController]
[Route("api/[controller]")]
public class InvitationsController : ControllerBase
{
    private readonly IInvitationService _invitationService;

    public InvitationsController(IInvitationService invitationService)
    {
        _invitationService = invitationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvitation(int organizationId, string userId)
    {
        var createdInvitation = await _invitationService.CreateInvitationAsync(organizationId, userId);
        return CreatedAtAction(nameof(GetInvitationById), new { id = createdInvitation.Id }, createdInvitation);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvitationById(int id)
    {
        var invitation = await _invitationService.GetByIdAsync(id);
        if (invitation == null)
        {
            return NotFound();
        }
        return Ok(invitation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvitation(int id, [FromBody] Invitation invitation)
    {
        if (id != invitation.Id)
        {
            return BadRequest();
        }
        var updatedInvitation = await _invitationService.UpdateAsync(invitation);
        return Ok(updatedInvitation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvitation(int id)
    {
        var deleted = await _invitationService.DeleteAsync(id);
        if (deleted == false)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{id}/accept")]
    public async Task<IActionResult> AcceptInvitation(int id)
    {
        var acceptedInvitation = await _invitationService.AcceptInvitationAsync(id);
        return Ok(acceptedInvitation);
    }

    [HttpPost("{id}/reject")]
    public async Task<IActionResult> RejectInvitation(int id)
    {
        var rejectedInvitation = await _invitationService.RejectInvitationAsync(id);
        return Ok(rejectedInvitation);
    }
}