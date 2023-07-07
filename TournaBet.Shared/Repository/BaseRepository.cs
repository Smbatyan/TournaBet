using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TournaBet.Shared.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }
}