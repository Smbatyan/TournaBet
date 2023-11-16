using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TournaBet.Auth.Options;

namespace TournaBet.Auth.Extensions;

internal static class AuthExtension
{
    internal static IServiceCollection AddAsymmetricJwtAuthentication(
        this IServiceCollection services)
    {
        AuthOptions authOptions = services.BuildServiceProvider()
            .GetRequiredService<IOptions<AuthOptions>>().Value;

        var rsaPublicKey = new RSACryptoServiceProvider();
        rsaPublicKey.FromXmlString(authOptions.PublicKey);

        var rsaSecurityKey = new RsaSecurityKey(rsaPublicKey);

        services.AddAuthentication()
            .AddJwtBearer("Asymmetric", o =>
            {
                o.IncludeErrorDetails = true;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = rsaSecurityKey,
                    RequireSignedTokens = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }
}