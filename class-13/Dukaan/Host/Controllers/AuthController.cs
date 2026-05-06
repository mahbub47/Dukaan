using Dukaan.Application.Dtos;
using Dukaan.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dukaan.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    public async Task<IActionResult> LoginWithJwt(LoginRequestDTO request)
    {
        var response = await authService.LoginAsync(request);
        return Ok(response);
    }
}
