using Microsoft.AspNetCore.Mvc;
using Organizations.API.Common.Models;

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

    [HttpGet("me")]
    public async Task<IActionResult> GetUser()
    {
        var user = new User
        {
            Id = "bb5d1b04-6f73-4ac0-acd2-e746ecf835f6",
            FirstName = "John",
            LastName = "Doe"
        };
        return Ok(user);
    }
}