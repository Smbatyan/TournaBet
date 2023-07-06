using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TournaBet.Auth.Models.Token;
using TournaBet.Auth.Services;

namespace TournaBet.Auth.Controllers;

[Authorize]
[Route("[module]/[controller]")]
internal class TokenController : Controller
{
    private readonly TokenService _tokenService;

    public TokenController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GetToken([FromBody]CreateTokenRequest createTokenRequest)
    {
        TokenResponse tokenResponse = await _tokenService.GenerateTokenAsync(createTokenRequest);

        return Ok(tokenResponse);
    }
}