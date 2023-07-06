namespace TournaBet.Auth.Options;

public class AuthOptions
{
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int ExpirationSeconds { get; set; }
    public int RefreshExpirationMonths { get; set; }

}