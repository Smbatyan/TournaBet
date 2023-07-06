namespace TournaBet.Auth.Models.Token;

public class CreateTokenRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}