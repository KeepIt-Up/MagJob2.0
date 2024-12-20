using Microsoft.AspNetCore.Mvc;
using Organizations.API.Common.Models;
using Organizations.API.Models;
using Organizations.API.Services;

namespace Organizations.API.Controllers;

public record GetMembersByOrganizationIdQuery(int OrganizationId) : QueryWithPaginationOptions;
public record GetMembersByNameQuery(string Name, int OrganizationId) : QueryWithPaginationOptions;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    public async Task<IActionResult> AddMember([FromBody] Member member)
    {
        return Ok(await _memberService.CreateAsync(member));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, [FromBody] UpdateMemberRequest request)
    {
        return Ok(await _memberService.UpdateMemberAsync(id, request));
    }

    [HttpPut("{id}/archive")]
    public async Task<IActionResult> ArchiveMember(int id)
    {
        await _memberService.ArchiveMemberAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(int id)
    {
        await _memberService.ArchiveMemberAsync(id);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember(int id)
    {
        return Ok(await _memberService.GetByIdAsync(id));
    }

    //TODO: Move to organization controller
    [HttpGet("search")]
    public async Task<IActionResult> GetMembers([FromQuery] GetMembersByNameQuery query)
    {
        return Ok(await _memberService.GetMembers(query.Name, query.OrganizationId, query.Options));
    }
}