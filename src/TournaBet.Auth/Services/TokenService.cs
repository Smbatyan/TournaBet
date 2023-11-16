using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TournaBet.Auth.Dto.Request;
using TournaBet.Auth.Dto.Response;
using TournaBet.Auth.Options;
using TournaBet.Shared.Models;

namespace TournaBet.Auth.Services;

internal class TokenService
{
    private readonly AuthOptions _authOptions;

    public TokenService(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    public async Task<TokenResponse> GenerateTokenAsync(CreateTokenRequest createTokenRequest)
    {
        PlayerEntity player = new()
        {
            Id = 1,
            Username = createTokenRequest.Username
        };

        TokenResponse tokenResponse = new()
        {
            AccessToken = GenerateAccessToken(player),
            RefreshToken = GenerateRefreshToken(player),
            UserId = player.Id.ToString(),
            ExpirationSeconds = 10000
        };

        return tokenResponse;
    }

    public async Task<TokenResponse> RefreshTokens(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var rsaPrivateKey = new RSACryptoServiceProvider();
        rsaPrivateKey.FromXmlString(_authOptions.PrivateKey);

        var rsaSecurityKey = new RsaSecurityKey(rsaPrivateKey);

        // Validate the refresh token
        ClaimsPrincipal claimsPrincipal;
        try
        {
            claimsPrincipal = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                IssuerSigningKey = rsaSecurityKey,
                ValidAudience = _authOptions.Audience,
                ValidIssuer = _authOptions.Issuer,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true
            }, out _);
        }
        catch (Exception ex)
        {
            throw new Exception("Invalid or expired refresh token", ex);
        }

        string newAccessToken = GenerateAccessToken(new PlayerEntity() {Id = 1});
        string newRefreshToken = GenerateRefreshToken(new PlayerEntity() {Id = 1});

        TokenResponse result = new()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            UserId = 1.ToString()
        };

        return result;
    }
    
    private string GenerateAccessToken(PlayerEntity player)
    {
        var rsaPrivateKey = new RSACryptoServiceProvider();
        rsaPrivateKey.FromXmlString(_authOptions.PrivateKey);

        var rsaSecurityKey = new RsaSecurityKey(rsaPrivateKey);

        var signingCredentials =
            new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var jwt = new JwtSecurityToken(
            audience: _authOptions.Audience,
            issuer: _authOptions.Issuer,
            claims: new Claim[]
            {
                new("userId", player.Id.ToString()),
                new("type", "access"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.UtcNow.AddSeconds(_authOptions.ExpirationSeconds),
            signingCredentials: signingCredentials
        );

        string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        return jwtToken;
    }

    private string GenerateRefreshToken(PlayerEntity player)
    {
        var rsaPrivateKey = new RSACryptoServiceProvider();
        rsaPrivateKey.FromXmlString(_authOptions.PrivateKey);

        var rsaSecurityKey = new RsaSecurityKey(rsaPrivateKey);

        var signingCredentials =
            new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var jwt = new JwtSecurityToken(
            audience: _authOptions.Audience,
            issuer: _authOptions.Issuer,
            claims: new Claim[]
            {
                new("deviceId", player.Id.ToString()),
                new("type", "refresh"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.UtcNow.AddMonths(_authOptions.RefreshExpirationMonths),
            signingCredentials: signingCredentials
        );

        string refreshToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        return refreshToken;
    }
}