using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TournaBet.Auth.Dto.Request;
using TournaBet.Auth.Dto.Response;
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
    
    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody]RefreshTokenRequest tokenRequest)
    {
        TokenResponse tokenResponse = await _tokenService.RefreshTokens(tokenRequest.RefreshToken);

        return Ok(tokenResponse);
    }
}