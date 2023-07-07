namespace TournaBet.Auth.Dto.Response;

public class TokenResponse
{
    public string UserId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int ExpirationSeconds { get; set; }
    
}