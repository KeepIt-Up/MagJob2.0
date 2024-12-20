using Microsoft.AspNetCore.Mvc;

namespace Organizations.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;
    public PermissionsController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken = default)
    {
        return Ok(await _permissionService.GetPermissions(cancellationToken));
    }
}