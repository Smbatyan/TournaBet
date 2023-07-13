using System.Linq.Expressions;

namespace TournaBet.Shared.Repository;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, 
        params Expression<Func<T, object>>[] includes);

    Task<T> FindOneAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}