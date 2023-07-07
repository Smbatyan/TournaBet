using TournaBet.Auth.Repositories.Abstraction;
using Tournabet.Auth.Shared.Services;
using TournaBet.Shared.Models;

namespace TournaBet.Auth.Services;

internal class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task ValidateUserAsync(int userId)
    {
        UserEntity user = await _authRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
    }
}