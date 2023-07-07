namespace TournaBet.Shared.Repository;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> GetRepository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync();
}