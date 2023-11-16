using TournaBet.Auth.Infrastructure;
using TournaBet.Auth.Repositories.Abstraction;
using TournaBet.Shared.Models;
using TournaBet.Shared.Repository;

namespace TournaBet.Auth.Repositories;

internal class AuthRepository : BaseRepository<PlayerEntity>, IAuthRepository
{
    public AuthRepository(AuthDbContext dbContext) : base(dbContext)
    {
    }
}