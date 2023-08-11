using TournaBet.Auth.Infrastructure;
using TournaBet.Auth.Repositories.Abstraction;
using TournaBet.Shared.Models;
using TournaBet.Shared.Repository;

namespace TournaBet.Auth.Repositories;

internal class AuthRepository : BaseRepository<PlayerEntity>, IAuthRepository
{
    public AuthRepository(IUnitOfWork unitOfWork, AuthDbContext dbContext) : base(dbContext)
    {
    }
}