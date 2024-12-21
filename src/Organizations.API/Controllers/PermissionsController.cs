using Microsoft.AspNetCore.Mvc;

namespace Organizations.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController(IMediator _mediator) : ControllerBase
{
    // [HttpGet]
    // public async Task<IActionResult> GetPermissions(GetPermissionsRequest request)
    // {
    //     return Ok(await _mediator.Send(request));
    // }
}