namespace TournaBet.Shared.Repository;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> GetRepository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync();
    public Task BeginTransactionAsync();
    public Task CommitTransactionAsync();
    public Task RollbackTransactionAsync();
}