using TournaBet.Shared.Models;
using TournaBet.Shared.Repository;

namespace TournaBet.Auth.Repositories.Abstraction;

public interface IAuthRepository : IBaseRepository<PlayerEntity>
{
    
}