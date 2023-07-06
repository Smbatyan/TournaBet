using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TournaBet.Auth.Models.Token;
using TournaBet.Auth.Options;
using TournaBet.Shared.Models;

namespace TournaBet.Auth.Services;

public class TokenService
{
    private readonly AuthOptions _authOptions;

    public TokenService(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    private string GenerateAccessTokenAsync(UserEntity user)
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
                new("userId", user.Login),
                new("deviceId", user.Id.ToString()),
                new("type", "access"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.UtcNow.AddSeconds(_authOptions.ExpirationSeconds),
            signingCredentials: signingCredentials
        );

        string jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        return jwtToken;
    }

    private string GenerateRefreshToken(UserEntity user)
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
                new("deviceId", user.Id.ToString()),
                new("type", "refresh"),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            },
            expires: DateTime.UtcNow.AddMonths(_authOptions.RefreshExpirationMonths),
            signingCredentials: signingCredentials
        );

        string refreshToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        return refreshToken;
    }

    public async Task<TokenResponse> GenerateTokenAsync(CreateTokenRequest createTokenRequest)
    {
        UserEntity user = new()
        {
            Id = 1,
            Login = createTokenRequest.Login
        };

        TokenResponse tokenResponse = new()
        {
            AccessToken = GenerateAccessTokenAsync(user),
            RefreshToken = GenerateRefreshToken(user),
            UserId = user.Id.ToString(),
            ExpirationSeconds = 10000
        };
        
        return tokenResponse;
    }
}