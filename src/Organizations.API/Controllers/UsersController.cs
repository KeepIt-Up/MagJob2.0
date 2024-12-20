using Microsoft.AspNetCore.Mvc;
using Organizations.API.Common.Models;
using Organizations.API.Services;

namespace Organizations.API.Controllers;

public record GetInvitationsByUserIdRequest(string UserId) : QueryWithPaginationOptions;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("invitations")]
    public async Task<IActionResult> GetInvitationsByUserId([FromQuery] GetInvitationsByUserIdRequest request)
    {
        var invitations = await _userService.GetInvitationsByUserIdAsync(request.UserId, request.Options);
        return Ok(invitations);
    }
}