namespace Tournabet.Auth.Shared.Services;

public interface IAuthService
{
    public Task ValidateUserAsync(int userId);
}